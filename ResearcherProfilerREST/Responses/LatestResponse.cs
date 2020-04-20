using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearcherProfilerREST.Responses
{
    public class LatestResponse
    {
        public List<LatestAggregateItem> aggregates { get; set; }
    }

    public class LatestAggregateItem
    {
        public string AggregateId { get; set; }

        public string AggregateName { get; set; }

        public string DateMeasured { get; set; }

        public double GlobalMean { get; set; }
        public double GlobalMedian { get; set; }
        public double GlobalMin { get; set; }
        public double GlobalMax { get; set; }
        public double GlobalStdDev { get; set; }

        public List<LatestMeasureItem> LastMeasures { get; set; }

    }
    public class LatestMeasureItem
    {
        public string Id { get; set; }
        public string PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ThresholdId { get; set; }
        public string ThresholdName { get; set; }
        public double Value { get; set; }

    }

}
