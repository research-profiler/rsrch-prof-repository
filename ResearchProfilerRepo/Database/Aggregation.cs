using System;
using System.Collections.Generic;

namespace ResearchProfilerRepo.Database
{
    public partial class Aggregation
    {
        public Aggregation()
        {
            GlobalMeasure = new HashSet<GlobalMeasure>();
            Threshold = new HashSet<Threshold>();
        }

        public Guid AggregationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AggregateType { get; set; }
        public string InputColumn { get; set; }
        public string InputCondition { get; set; }
        public Guid? ParentAggregation { get; set; }

        public virtual Aggregation AggregationNavigation { get; set; }
        public virtual Aggregation InverseAggregationNavigation { get; set; }
        public virtual ICollection<GlobalMeasure> GlobalMeasure { get; set; }
        public virtual ICollection<Threshold> Threshold { get; set; }
    }
}
