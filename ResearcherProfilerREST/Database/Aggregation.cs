using System;
using System.Collections.Generic;

namespace ResearcherProfilerREST.Database
{
    public partial class Aggregation
    {
        public Aggregation()
        {
            GlobalMeasure = new HashSet<GlobalMeasure>();
            InverseParentNameNavigation = new HashSet<Aggregation>();
            Measure = new HashSet<Measure>();
            Threshold = new HashSet<Threshold>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Guid? ParentName { get; set; }
        public string CsvColumn { get; set; }
        public string Filter { get; set; }

        public virtual Aggregation ParentNameNavigation { get; set; }
        public virtual ICollection<GlobalMeasure> GlobalMeasure { get; set; }
        public virtual ICollection<Aggregation> InverseParentNameNavigation { get; set; }
        public virtual ICollection<Measure> Measure { get; set; }
        public virtual ICollection<Threshold> Threshold { get; set; }
    }
}
