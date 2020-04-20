using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearcherProfilerREST.Responses
{
    public class ThresholdResponse
    {
        public List<ThresholdResponseItem> Thresholds { get; set; }
    }

    public class ThresholdResponseItem
    {
        public string ThresholdId { get; set; }
        public string ThresholdName { get; set; }
        public double Start { get; set; }
        public double End { get; set; }
    }
}
