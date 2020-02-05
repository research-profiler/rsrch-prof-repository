using System;
using System.Collections.Generic;

namespace ResearchProfilerRepo.Database
{
    public partial class Threshold
    {
        public Guid AggregateId { get; set; }
        public Guid ThresholdId { get; set; }
        public string Name { get; set; }
        public double StartValue { get; set; }
        public double EndValue { get; set; }

        public virtual Aggregation Aggregate { get; set; }
    }
}
