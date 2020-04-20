using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearcherProfilerREST.Responses
{
    public class ResearchersResponse
    {
        public List<ResearchersItem> Researchers { get; set; }
    }

    public class ResearchersItem
    {
        public string mnumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
    }
}
