﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CQS.Genome.Sam;
using RCPA;

namespace CQS.Genome.Mapping
{
  public class ChromosomeCountProcessor : AbstractThreadProcessor
  {
    private ChromosomeCountProcessorOptions options;

    public ChromosomeCountProcessor(ChromosomeCountProcessorOptions options)
    {
      this.options = options;
    }

    public override IEnumerable<string> Process()
    {
      var result = new List<string>();

      var builder = options.GetCandidateBuilder();
      var queries = new HashSet<string>();
      var samitems = builder.Build<SAMAlignedItem>(options.InputFile, out queries);
      if (options.Offsets.Count > 0 && options.Offsets[0] != 0)
      {
        var offsets = new HashSet<long>(options.Offsets);
        samitems.ForEach(m =>
        {
          m.RemoveLocation(l => !offsets.Contains(l.Start));
        });
      }

      var cm = options.GetCountMap();
      samitems.ForEach(m =>
      {
        if(m.Locations.Any(l => IsHuman(l))){
          m.RemoveLocation(l => !IsHuman(l));
        }
        m.QueryCount = cm.GetCount(m.Qname);
        m.SortLocations();
      });

      var locs = (from s in samitems
                  from l in s.Locations
                  select l).ToList();

      var chroms = (from g in locs.GroupBy(m => m.Seqname)
                    select new ChromosomeCountItem() { Names = new HashSet<string>(new string[] { g.Key }), Queries = new HashSet<SAMAlignedItem>(from l in g select l.Parent) }).ToList();
      chroms.Sort((m1, m2) => m2.Queries.Count.CompareTo(m1.Queries.Count));

      var xml = Path.ChangeExtension(options.OutputFile, "xml");
      new ChromosomeCountXmFormat().WriteToFile(xml, chroms);

      chroms.MergeItems();

      chroms.Sort((m1, m2) => m2.QueryCount.CompareTo(m1.QueryCount));
      using (var sw = new StreamWriter(options.OutputFile))
      {
        sw.WriteLine("Name\tQueryCount");
        foreach (var mirna in chroms)
        {
          if (mirna.Names.Any(m => m.StartsWith("hsa")))
          {
            mirna.Names.RemoveWhere(m => !m.StartsWith("hsa"));
          }

          sw.WriteLine((from m in mirna.Names orderby m select m).Merge(";") + "\t" + mirna.QueryCount);
        }
      }

      Progress.End();

      return result;
    }


    private static bool IsHuman(SAMAlignedLocation l)
    {
      return l.Seqname.StartsWith("hsa");
    }
  }
}