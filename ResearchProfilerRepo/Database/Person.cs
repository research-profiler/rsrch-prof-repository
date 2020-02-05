using System;
using System.Collections.Generic;

namespace ResearchProfilerRepo.Database
{
    public partial class Person
    {
        public Person()
        {
            Measure = new HashSet<Measure>();
        }

        public string Ucid { get; set; }
        public string Email { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public string Department { get; set; }

        public virtual ICollection<Measure> Measure { get; set; }
    }
}
