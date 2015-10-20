﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CQS.Genome.SmallRNA;

namespace CQS.Genome
{
  public class CountMap
  {
    private string countFile;

    public Dictionary<string, int> Counts { get; private set; }

    public bool HasCountFile { get; private set; }

    public CountMap()
      : this(null)
    {
      Counts = new Dictionary<string, int>();
    }

    public CountMap(string countFile)
    {
      this.countFile = countFile;
      this.HasCountFile = File.Exists(countFile);
      this.Counts = new Dictionary<string, int>();

      if (this.HasCountFile)
      {
        Dictionary<string, string> counts = new MapReader(0, 1).ReadFromFile(countFile);
        foreach (var c in counts)
        {
          Counts[c.Key] = int.Parse(c.Value);
        }
        func = DoGetIdenticalCount;
      }
      else
      {
        func = DoGetCountOne;
      }
    }

    private int DoGetCountOne(params string[] qNames)
    {
      return 1;
    }

    private int DoGetIdenticalCount(params string[] qNames)
    {
      int value;
      foreach (var qName in qNames)
      {
        if (!string.IsNullOrEmpty(qName) && Counts.TryGetValue(qName, out value))
        {
          return value;
        }
      }

      throw new Exception("Cannot find query " + qNames.Where(l => !string.IsNullOrEmpty(l)).Distinct().Merge("/") + " in count file " + this.countFile);
    }

    delegate int GetCountFunc(params string[] qNames);

    private GetCountFunc func;

    public int GetCount(params string[] qNames)
    {
      return func(qNames);
    }
  }
}
