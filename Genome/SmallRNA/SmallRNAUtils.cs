﻿using CQS.Genome.Feature;
using CQS.Genome.Sam;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CQS.Genome.SmallRNA
{
  public static class SmallRNAUtils
  {
    public static void InitializeSmallRnaNTA(IEnumerable<SAMAlignedItem> reads)
    {
      foreach (var m in reads)
      {
        if (m.Qname.Contains(SmallRNAConsts.NTA_TAG))
        {
          m.OriginalQname = m.Qname.StringBefore(SmallRNAConsts.NTA_TAG);
          m.NTA = m.Qname.StringAfter(SmallRNAConsts.NTA_TAG);
        }
      }
    }

    private static Regex tRNACodeRegex1 = new Regex(@"\-([A-Za-z]{3,})[\-]{0,1}([A-Za-z]{3})(-|\z)");
    private static Regex tRNACodeRegex2 = new Regex(@"tRNA[0-9]*\-(.+)", RegexOptions.IgnoreCase);
    public static string GetTrnaAnticodon(string name)
    {
      var m = tRNACodeRegex1.Match(name);
      if (!m.Success)
      {
        var m2 = tRNACodeRegex2.Match(name);
        if (!m2.Success)
        {
          Console.Error.WriteLine("Warning: cannot find tRNA code from " + name);
          return name;
        }
        else
        {
          return m2.Groups[1].Value;
        }
      }
      else
      {
        return m.Groups[1].Value + m.Groups[2].Value;
      }
    }

    public static string GetTrnaAnticodon(FeatureItem item)
    {
      return GetTrnaAnticodon(item.Name);
    }

    private static Regex tRNAAminoacidRegex1 = new Regex(@"\-([A-Za-z()]{3,})[\-]{0,1}[?A-Za-z]{3}(-|\z)");
    private static Regex tRNAAminoacidRegex2 = new Regex(@"tRNA[0-9]*\-(.+)", RegexOptions.IgnoreCase);
    public static string GetTRNAAminoacid(string name)
    {
      var m = tRNAAminoacidRegex1.Match(name);
      if (!m.Success)
      {
        var m2 = tRNAAminoacidRegex2.Match(name);
        if (!m2.Success)
        {
          Console.Error.WriteLine("Warning: cannot find tRNA code from " + name);
          return name;
        }
        else
        {
          return m2.Groups[1].Value;
        }
      }
      else
      {
        return m.Groups[1].Value;
      }
    }

    public static string GetTrnaAminoacid(FeatureItem item)
    {
      return GetTRNAAminoacid(item.Name);
    }

    public static Dictionary<string, Dictionary<string, FeatureItemGroup>> GroupByTRNACode(Dictionary<string, Dictionary<string, FeatureItemGroup>> dic, bool updateGroupName = false)
    {
      var result = new Dictionary<string, Dictionary<string, FeatureItemGroup>>();
      foreach (var d in dic)
      {
        var newmap = (from fig in d.Value
                      from fi in fig.Value
                      select fi).GroupByFunction(GetTrnaAnticodon, updateGroupName).ToDictionary(m => m.DisplayName);
        result[d.Key] = newmap;
      }
      return result;
    }

    public static Dictionary<string, Dictionary<string, FeatureItemGroup>> GroupByIdenticalQuery(Dictionary<string, Dictionary<string, FeatureItemGroup>> dic)
    {
      var featureNames = (from d in dic.Values
                          from fig in d.Values
                          from fi in fig
                          select fi.Name).Distinct().ToList();

      var map = new Dictionary<string, FeatureItem>();
      foreach (var featureName in featureNames)
      {
        var item = new FeatureItem() { Name = featureName };
        item.Locations.Add(new FeatureLocation());
        map[featureName] = item;
      }

      foreach (var d in dic.Values)
      {
        foreach (var fig in d.Values)
        {
          foreach (var fi in fig)
          {
            var item = map[fi.Name];
            item.Locations[0].SamLocations.AddRange(from l in fi.Locations from ll in l.SamLocations select ll);
          }
        }
      }

      var groups = map.Values.GroupByIdenticalQuery();

      var result = new Dictionary<string, Dictionary<string, FeatureItemGroup>>();
      foreach (var sample in dic.Keys)
      {
        var oldfeatures = (from v in dic[sample].Values
                           from vv in v
                           select vv).ToDictionary(m => m.Name);

        var newdic = new Dictionary<string, FeatureItemGroup>();
        result[sample] = newdic;

        foreach (var g in groups)
        {
          var findfeatures = (from item in g
                              where oldfeatures.ContainsKey(item.Name)
                              select oldfeatures[item.Name]).ToList();
          if (findfeatures.Count > 0)
          {
            var newgroup = new FeatureItemGroup();
            newgroup.AddRange(findfeatures);
            newdic[g.DisplayName] = newgroup;
          }
        }
      }

      return result;
    }

    public static void SelectBestMatchedNTA(List<FeatureLocation> smallRNAs)
    {
      var mappedReads = (from m in smallRNAs
                         from l in m.SamLocations
                         let loc = l.SamLocation
                         select new { Loc = loc, Qname = loc.Parent.Qname.StringBefore(SmallRNAConsts.NTA_TAG), NTA_Length = loc.Parent.Qname.StringAfter(SmallRNAConsts.NTA_TAG).Length }).ToList();

      var map = mappedReads.GroupBy(m => m.Qname);

      var sals = new HashSet<SAMAlignedLocation>();
      foreach (var locs in map)
      {
        //get smallest number of mismatch, then get shortest NTA
        var ntas = locs.GroupBy(m => m.Loc.NumberOfMismatch).OrderBy(m => m.Key).First().GroupBy(m => m.NTA_Length).OrderBy(m => m.Key).First();
        foreach (var nta in ntas)
        {
          sals.Add(nta.Loc);
        }
      }

      foreach (var m in smallRNAs)
      {
        foreach (var l in new List<FeatureSamLocation>(m.SamLocations))
        {
          if (!sals.Contains(l.SamLocation))
          {
            m.SamLocations.Remove(l);
            l.SamLocation.Features.Remove(l.FeatureLocation);
          }
        }
      }
    }

    public static HashSet<SAMAlignedItem> GetMappedReads(List<FeatureLocation> features)
    {
      return new HashSet<SAMAlignedItem>(from m in features
                                         from l in m.SamLocations
                                         select l.SamLocation.Parent);
    }

    public static void RemoveReadsFromMap(Dictionary<string, Dictionary<char, List<SAMAlignedLocation>>> chrStrandMatchedMap, HashSet<SAMAlignedItem> mappedReads)
    {
      var qnames = new HashSet<string>(from r in mappedReads select r.OriginalQname);

      foreach (var chr in chrStrandMatchedMap.Keys)
      {
        var strandMap = chrStrandMatchedMap[chr];
        foreach (var strand in strandMap.Keys)
        {
          var locs = strandMap[strand];
          locs.RemoveAll(m => qnames.Contains(m.Parent.OriginalQname));
        }
      }
    }


    public static string[] GetOutputBiotypes(bool exportYRNA)
    {
      var result = new List<string>();
      if (exportYRNA)
      {
        result.Add(SmallRNABiotype.yRNA.ToString());
      }
      result.Add(SmallRNABiotype.snRNA.ToString());
      result.Add(SmallRNABiotype.snoRNA.ToString());
      result.Add(SmallRNABiotype.rRNA.ToString());
      return result.ToArray();
    }
  }
}
