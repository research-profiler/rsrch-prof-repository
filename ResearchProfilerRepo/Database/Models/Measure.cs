using System;
using System.Collections.Generic;

namespace ResearchProfilerRepo.Database.Models
{
    public class Measure : IModel<Guid>
    {
        public Guid Id { get; set; }
        public DateTime DateMeasured { get; set; }
        public string PersonMeasured { get; set; }
        public Guid AggregateMeasured { get; set; }
        public double Value { get; set; }

        public virtual Aggregation AggregateMeasuredNavigation { get; set; }
        public virtual Person PersonMeasuredNavigation { get; set; }

        public Guid GetPrimaryKey()
        {
            return Id;
        }
    }
}
