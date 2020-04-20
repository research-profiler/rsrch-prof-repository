using System;
using System.Collections.Generic;

namespace ResearchProfilerRepo.Database.Models
{
    public partial class Person : IModel<string>
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

        public string GetPrimaryKey()
        {
            return Mnumber;
        }
    }
}
