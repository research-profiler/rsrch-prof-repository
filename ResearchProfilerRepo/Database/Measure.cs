using System;
using System.Collections.Generic;

namespace ResearchProfilerRepo.Database
{
    public partial class Measure
    {
        public string PersonId { get; set; }
        public Guid AggregateId { get; set; }
        public DateTimeOffset DateMeasured { get; set; }
        public double Value { get; set; }
        public string JobTitle { get; set; }

        public virtual Person Person { get; set; }
    }
}
