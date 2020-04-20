using System;
using System.Collections.Generic;

namespace ResearchProfilerRepo.Database.Models
{

    public class GlobalMeasure : IModel<Guid>
    {
        public Guid Id { get; set; }
        public DateTime DateMeasured { get; set; }
        public Guid AggregateId { get; set; }
        public double Mean { get; set; }
        public double Median { get; set; }
        public double MinimumValue { get; set; }
        public double MaximumValue { get; set; }
        public double StandardDeviation { get; set; }

        public virtual Aggregation Aggregate { get; set; }

        public Guid GetPrimaryKey()
        {
            return Id;
        }
    }
}
