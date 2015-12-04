﻿using CQS.Genome.Bed;
using CQS.Genome.Feature;
using CQS.Genome.SmallRNA;
using RCPA;
using RCPA.Seq;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQS.Genome.Parclip
{
  public static class ParclipUtils
  {
    public static List<CoverageRegion> GetSmallRNACoverageRegion(string mappedFeatureXmlFile, string[] includeSmallRNATags, string[] excludeSmallRNATags)
    {
      var result = new List<CoverageRegion>();

      var smallRNAGroups = new FeatureItemGroupXmlFormat().ReadFromFile(mappedFeatureXmlFile);
      if (excludeSmallRNATags != null && excludeSmallRNATags.Length > 0)
      {
        smallRNAGroups.ForEach(m => m.RemoveAll(l => excludeSmallRNATags.Any(k => l.Name.StartsWith(k))));
      }
      if (includeSmallRNATags != null && includeSmallRNATags.Length > 0)
      {
        smallRNAGroups.ForEach(m => m.RemoveAll(l => !includeSmallRNATags.Any(k => l.Name.StartsWith(k))));
      }
      smallRNAGroups.RemoveAll(m => m.Count == 0);

      foreach (var sg in smallRNAGroups)
      {
        //since the items in same group shared same reads, only the first one will be used.
        var smallRNA = sg[0];
        smallRNA.Name = (from g in sg select g.Name).Merge("/");

        smallRNA.Locations.RemoveAll(m => m.SamLocations.Count == 0);
        smallRNA.CombineLocationByMappedReads();

        //only first location will be used.
        var loc = smallRNA.Locations[0];

        //coverage in all position will be set as same as total query count
        var rg = new CoverageRegion();
        rg.Name = smallRNA.Name;
        rg.Seqname = loc.Seqname;
        rg.Start = loc.Start;
        rg.End = loc.End;
        rg.Strand = loc.Strand;
        rg.Sequence = loc.Sequence;

        var coverage = (from sloc in loc.SamLocations select sloc.SamLocation.Parent.QueryCount).Sum();

        for (int i = 0; i < loc.Length; i++)
        {
          rg.Coverages.Add(coverage);
        }
        result.Add(rg);
      }
      return result;
    }

    public static SeedItem GetSeed(CoverageRegion cr, int offset, int seedLength, double minCoverage)
    {
      if (cr.Sequence.Length < offset + seedLength)
      {
        return null;
      }

      var coverage = cr.Coverages.Skip(offset).Take(seedLength).Average();
      if (coverage < minCoverage)
      {
        return null;
      }

      var newseq = cr.Sequence.Substring(offset, seedLength);
      var start = cr.Start + offset;
      var end = cr.Start + offset + seedLength - 1;
      if (cr.Strand == '+')
      {
        newseq = SequenceUtils.GetReverseComplementedSequence(newseq);
      }

      return new SeedItem()
      {
        Seqname = cr.Seqname,
        Start = start,
        End = end,
        Strand = cr.Strand,
        Coverage = coverage,
        Name = cr.Name,
        Sequence = newseq,
        Source = cr,
        SourceOffset = offset,
        GeneSymbol = cr.GeneSymbol
      };
    }

    public static List<CoverageRegion> GetTargetCoverageRegion(ITargetBuilderOptions options, IProgressCallback progress)
    {
      List<CoverageRegion> result;
      if (options.TargetFile.EndsWith(".xml"))
      {
        result = GetTargetCoverageRegionFromXml(options, progress);
      }
      else
      {
        result = GetTargetCoverageRegionFromBed(options, progress);
      }

      var dic = result.ToGroupDictionary(m => m.Seqname);

      progress.SetMessage("Filling sequence from {0}...", options.GenomeFastaFile);
      using (var sr = new StreamReader(options.GenomeFastaFile))
      {
        var ff = new FastaFormat();
        Sequence seq;
        while ((seq = ff.ReadSequence(sr)) != null)
        {
          progress.SetMessage("Processing chromosome {0} ...", seq.Reference);
          var seqname = seq.Name.StringAfter("chr");
          List<CoverageRegion> lst;
          if (dic.TryGetValue(seqname, out lst))
          {
            foreach (var l in lst)
            {
              l.Sequence = seq.SeqString.Substring((int)(l.Start - 1), (int)l.Length);
            }
          }
        }
      }
      progress.SetMessage("Filling sequence finished.");

      return result;
    }

    public static List<CoverageRegion> GetTargetCoverageRegionFromXml(ITargetBuilderOptions options, IProgressCallback progress)
    {
      var result = new List<CoverageRegion>();

      var groups = new FeatureItemGroupXmlFormat().ReadFromFile(options.TargetFile);
      progress.SetMessage("Total {0} potential target group read from file {1}", groups.Count, options.TargetFile);

      foreach (var group in groups)
      {
        //since the items in same group shared same reads, only the first one will be used.
        for (int i = 1; i < group.Count; i++)
        {
          group[0].Name = group[0].Name + "/" + group[i].Name;
        }

        group.RemoveRange(1, group.Count - 1);

        var utr = group[0];

        utr.Locations.RemoveAll(m => m.SamLocations.Count == 0);
        utr.CombineLocationByMappedReads();

        foreach (var loc in utr.Locations)
        {
          var map = new Dictionary<long, int>();
          foreach (var sloc in loc.SamLocations)
          {
            for (long i = sloc.SamLocation.Start; i <= sloc.SamLocation.End; i++)
            {
              int count;
              if (map.TryGetValue(i, out count))
              {
                map[i] = count + sloc.SamLocation.Parent.QueryCount;
              }
              else
              {
                map[i] = sloc.SamLocation.Parent.QueryCount;
              }
            }
          }

          var keys = (from k in map.Keys
                      orderby k
                      select k).ToList();

          int start = 0;
          int end = start + 1;
          while (true)
          {
            if (end == keys.Count || keys[end] != keys[end - 1] + 1)
            {
              var rg = new CoverageRegion();
              rg.Name = utr.Name;
              rg.Seqname = loc.Seqname;
              rg.Start = keys[start];
              rg.End = keys[end - 1];
              rg.Strand = loc.Strand;
              for (int i = start; i < end; i++)
              {
                rg.Coverages.Add(map[keys[i]]);
              }
              result.Add(rg);

              if (end == keys.Count)
              {
                break;
              }

              start = end;
              end = start + 1;
            }
            else
            {
              end++;
            }
          }
        }
      }

      return result;
    }

    public static List<CoverageRegion> GetTargetCoverageRegionFromBed(ITargetBuilderOptions options, IProgressCallback progress)
    {
      var result = new List<CoverageRegion>();

      var groups = new BedItemFile<BedItem>().ReadFromFile(options.TargetFile);
      progress.SetMessage("Total {0} potential target group read from file {1}", groups.Count, options.TargetFile);

      foreach (var utr in groups)
      {
        var rg = new CoverageRegion();
        rg.Name = utr.Name;
        rg.Seqname = utr.Seqname.StringAfter("chr");
        rg.Start = utr.Start;
        rg.End = utr.End;
        rg.Strand = utr.Strand;
        for (var i = rg.Start; i <= rg.End; i++)
        {
          rg.Coverages.Add(1000);
        }
        result.Add(rg);
      }

      return result;
    }

    public static List<SeedItem> BuildTargetSeeds(ITargetBuilderOptions options, Func<SeedItem, bool> acceptSeed, IProgressCallback progress)
    {
      List<SeedItem> seeds = new List<SeedItem>();

      var mapped = GetTargetCoverageRegion(options, progress);

      var namemap = new MapReader(1, 12).ReadFromFile(options.RefgeneFile);

      mapped.ForEach(m =>
      {
        var gene = m.Name.StringBefore("_utr3");
        m.GeneSymbol = namemap.ContainsKey(gene) ? namemap[gene] : string.Empty;
      });

      progress.SetMessage("Building seeds ...");
      progress.SetRange(0, mapped.Count);
      progress.SetPosition(0);
      foreach (var l in mapped)
      {
        progress.Increment(1);
        for (int i = 0; i < l.Sequence.Length - options.MinimumSeedLength; i++)
        {
          SeedItem si = ParclipUtils.GetSeed(l, i, options.MinimumSeedLength, options.MinimumCoverage);

          if (si != null && acceptSeed(si))
          {
            seeds.Add(si);
          }
        }
      }
      progress.End();
      progress.SetMessage("Total {0} {1}mers seeds were built.", seeds.Count, options.MinimumSeedLength);

      return seeds;
    }

    public static Dictionary<string, List<SeedItem>> BuildTargetSeedMap(ITargetBuilderOptions options, Func<SeedItem, bool> acceptSeed, IProgressCallback progress)
    {
      //Read 6 mers from target
      var targetSeeds = BuildTargetSeeds(options, acceptSeed, progress);
      progress.SetMessage("Grouping seeds by sequence ...");
      var result = targetSeeds.ToGroupDictionary(m => m.Sequence.ToUpper());
      progress.SetMessage("Total {0} unique {1}mers seeds were built.", result.Count, options.MinimumSeedLength);
      return result;
    }

    public static List<SeedItem> FindLongestTarget(List<SeedItem> target, CoverageRegion t2c, string seq, int offset, int minimumSeedLength, int maximumSeedLength, double minimumCoverage)
    {
      var extendSeedLength = minimumSeedLength;
      while (extendSeedLength < maximumSeedLength)
      {
        extendSeedLength++;

        //check the coverage in smallRNA
        if (t2c != null)
        {
          var extendCoverage = t2c.Coverages.Skip(offset).Take(extendSeedLength).Average();
          if (extendCoverage < minimumCoverage)
          {
            break;
          }
        }

        var extendSeed = seq.Substring(offset, extendSeedLength);

        var extendTarget = new List<SeedItem>();
        foreach (var utrTarget in target)
        {
          var newoffset = utrTarget.Strand == '-' ? utrTarget.SourceOffset : utrTarget.SourceOffset - 1;
          if (newoffset < 0)
          {
            continue;
          }

          var newseed = GetSeed(utrTarget.Source, newoffset, extendSeedLength, minimumCoverage);
          if (newseed == null)
          {
            continue;
          }

          if (extendSeed.Equals(newseed.Sequence))
          {
            extendTarget.Add(newseed);
          }
        }

        if (extendTarget.Count > 0)
        {
          target = extendTarget;
        }
        else
        {
          break;
        }
      }
      return target;
    }
  }
}
