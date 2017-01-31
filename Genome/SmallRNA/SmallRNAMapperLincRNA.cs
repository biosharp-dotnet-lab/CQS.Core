﻿using CQS.Genome.Feature;
using CQS.Genome.Sam;
using System;
using System.Linq;
using System.Collections.Generic;

namespace CQS.Genome.SmallRNA
{
  public class SmallRNAMapperLincRNA : SmallRNAMapperLongRNA
  {
    public SmallRNAMapperLincRNA(ISmallRNACountProcessorOptions options) : base("lincRNA", options, feature => feature.Category.Equals(SmallRNAConsts.lincRNA))
    { }

    public override void MapReadToFeature(List<FeatureLocation> features, Dictionary<string, Dictionary<char, List<SAMAlignedLocation>>> chrStrandReadMap)
    {
      base.MapReadToFeature(features, chrStrandReadMap);

      //only if the all samlocations mapped to lincRNA, that query will be kept.
      var allSamLocations = new HashSet<SAMAlignedLocation>(from m in features
                                                            from l in m.SamLocations
                                                            select l.SamLocation);

      var allSams = new HashSet<SAMAlignedItem>(from m in allSamLocations
                                                select m.Parent);

      int removed = 0;
      foreach (var sam in allSams)
      {
        if (sam.Locations.Any(l => !allSamLocations.Contains(l)))
        {
          removed++;
          foreach (var fea in features)
          {
            fea.SamLocations.RemoveAll(l => l.SamLocation.Parent == sam);
          }

          foreach (var loc in sam.Locations)
          {
            loc.Features.Clear();
          }
        }
      }

      Progress.SetMessage("{0} of {1} queries were removed from lincRNA mapping due to ambigious mapped.", removed, allSams.Count);
    }
  }
}
