using System;
using System.Collections.Generic;

namespace ResearcherProfilerREST.Database
{
    public partial class Threshold
    {
        public Guid Id { get; set; }
        public double ThresholdStart { get; set; }
        public double ThresholdEnd { get; set; }
        public string ThresholdName { get; set; }
        public Guid Aggregation { get; set; }

        public virtual Aggregation AggregationNavigation { get; set; }
    }
}
