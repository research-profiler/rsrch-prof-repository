using System;
using System.Collections.Generic;

namespace ResearchProfilerRepo.Database
{
    public partial class GlobalMeasure
    {
        public Guid AggregateId { get; set; }
        public DateTime DateMeasured { get; set; }
        public double Median { get; set; }
        public double Range { get; set; }
        public double Mean { get; set; }
        public double StandardDeviation { get; set; }

        public virtual Aggregation Aggregate { get; set; }
    }
}
