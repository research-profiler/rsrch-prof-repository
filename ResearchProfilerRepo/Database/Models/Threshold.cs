using System;
using System.Collections.Generic;

namespace ResearchProfilerRepo.Database.Models
{
    public partial class Threshold : IModel<Guid>
    {
        public Guid Id { get; set; }
        public double ThresholdStart { get; set; }
        public double ThresholdEnd { get; set; }
        public string ThresholdName { get; set; }
        public Guid Aggregation { get; set; }

        public virtual Aggregation AggregationNavigation { get; set; }

        public Guid GetPrimaryKey()
        {
            return Id;
        }
    }
}
