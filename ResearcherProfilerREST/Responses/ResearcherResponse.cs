using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearcherProfilerREST.Responses
{
    public class ResearcherResponse
    {
        public string Mnumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public List<ResearcherLatestMeasureItem> LastMeasures { get; set; }
    }

    public class ResearcherLatestMeasureItem
    {
        public string Id { get; set; }
        public string DateMeasured { get; set; }
        public double value { get; set; }
        public double GlobalMedian { get; set; }
        public double GlobalMean { get; set; }
        public double GlobalMin { get; set; }
        public double GlobalMax { get; set; }
        public double GlobalStdDev { get; set; }
        public string AggregateId { get; set; }
        public string AggregateName { get; set; }
        public string AggregateType { get; set; }
        public string ThresholdId { get; set; }
        public string ThresholdName { get; set; }
    }
}
