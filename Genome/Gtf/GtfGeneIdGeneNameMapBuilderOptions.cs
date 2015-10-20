﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Commandline;
using CommandLine;
using System.IO;

namespace CQS.Genome.Gtf
{
  public class GtfGeneIdGeneNameMapBuilderOptions : AbstractOptions
  {
    [Option('i', "InputFile", Required = true, MetaValue = "FILE", HelpText = "Input gtf file")]
    public string InputFile { get; set; }

    [Option('o', "OutputPrefix", Required = true, MetaValue = "FILE", HelpText = "Output map file")]
    public string OutputFile { get; set; }

    [Option('m', "MapFile", Required = false, MetaValue = "FILE", HelpText = "Additional id/name map file")]
    public string MapFile { get; set; }

    public override bool PrepareOptions()
    {
      if (!File.Exists(this.InputFile))
      {
        ParsingErrors.Add(string.Format("Input file not exists {0}.", this.InputFile));
      }

      if (!string.IsNullOrEmpty(this.MapFile) && !File.Exists(this.MapFile))
      {
        ParsingErrors.Add(string.Format("Map file not exists {0}.", this.MapFile));
      }

      return ParsingErrors.Count == 0;
    }
  }
}
