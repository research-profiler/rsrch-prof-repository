using System;
using System.Collections.Generic;

namespace ResearcherProfilerREST.Database
{
    public partial class Person
    {
        public Person()
        {
            Measure = new HashSet<Measure>();
        }

        public string Mnumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }

        public virtual ICollection<Measure> Measure { get; set; }
    }
}
