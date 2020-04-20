using ResearcherProfilerREST.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearcherProfilerREST.Database
{
    public class ApiRespository
    {
        public LatestResponse GetLatest()
        {
            var dbContext = GetDBContext();

            LatestResponse response = new LatestResponse();
            List<LatestAggregateItem> latestAggregateItems = new List<LatestAggregateItem>();

            List<Aggregation> aggregations = dbContext.Aggregation.ToList();
            foreach (Aggregation aggregation in aggregations)
            {
                GlobalMeasure global = dbContext.GlobalMeasure
                    .Where(g => g.AggregateId == aggregation.Id && g.DateMeasured == DateTime.Today).SingleOrDefault();
                if (global == default(GlobalMeasure)) break;
                List<LatestMeasureItem> latestMeasures = new List<LatestMeasureItem>();

                List<Measure> measures = dbContext.Measure
                    .Where(m => m.AggregateMeasured == aggregation.Id && m.DateMeasured == DateTime.Today).ToList();

                foreach (Measure measure in measures)
                {
                    Threshold threshold = dbContext.Threshold.Where(t => t.Aggregation == aggregation.Id)
                        .Where(t => t.ThresholdStart < measure.Value && measure.Value <= t.ThresholdEnd).Single();
                    Person person = dbContext.Person.Find(measure.PersonMeasured);
                    Random random = new Random();
                    LatestMeasureItem latestMeasure = new LatestMeasureItem
                    {
                        Id = measure.Id.ToString(),
                        PersonId = "person.Mnumber",
                        FirstName = "person.FirstName",
                        LastName = "person.LastName",
                        ThresholdId = threshold.Id.ToString(),
                        ThresholdName = threshold.ThresholdName,
                        Value = measure.Value + random.Next(-5000, 5000)
                    };
                    latestMeasures.Add(latestMeasure);
                }
                LatestAggregateItem latestAggregateItem = new LatestAggregateItem
                {
                    AggregateId = aggregation.Id.ToString(),
                    AggregateName = aggregation.Name,
                    DateMeasured = DateTime.Today.ToString("yyyy-MM-dd"),
                    GlobalMean = global.Mean,
                    GlobalMedian = global.Median,
                    GlobalMax = global.MaximumValue,
                    GlobalMin = global.MinimumValue,
                    GlobalStdDev = global.StandardDeviation,
                    LastMeasures = latestMeasures
                };
                latestAggregateItems.Add(latestAggregateItem);
            }
            LatestResponse latestResponse = new LatestResponse
            {
                aggregates = latestAggregateItems
            };
            return latestResponse;
        }

        public ResearchersResponse GetResearchers(string query)
        {
            ResearchersResponse response = new ResearchersResponse
            {
                Researchers = new List<ResearchersItem>()
            };

            var dbContext = GetDBContext();

            List<Person> people = dbContext.Person.ToList().Where(p => MatchPersonQuery(p, query)).ToList();

            foreach (var person in people)
            {
                ResearchersItem researchersItem = new ResearchersItem
                {
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Department = person.Department,
                    Email = person.Email,
                    mnumber = person.Mnumber,
                };
                response.Researchers.Add(researchersItem);
            }

            return response;
        }

        public ThresholdResponse GetThresholds(Guid aggregateId)
        {
            var dbContext = GetDBContext();

            ThresholdResponse response = new ThresholdResponse
            {
                Thresholds = new List<ThresholdResponseItem>()
            };

            List<Threshold> thresholds = dbContext.Threshold.Where(t => t.Aggregation == aggregateId).ToList();
            foreach (var threshold in thresholds)
            {
                ThresholdResponseItem thresholdResponseItem = new ThresholdResponseItem
                {
                    ThresholdId = threshold.Id.ToString(),
                    Start = threshold.ThresholdStart,
                    End = threshold.ThresholdEnd,
                    ThresholdName = threshold.ThresholdName
                };
                response.Thresholds.Add(thresholdResponseItem);
            }
            return response;
        }

        public ResearcherResponse GetResearcher(string mnumber)
        {
            var dbContext = GetDBContext();

            mnumber = mnumber.ToUpper();

            Person person = dbContext.Person.Find(mnumber);
            ResearcherResponse response = new ResearcherResponse
            {
                Mnumber = person.Mnumber,
                FirstName = person.FirstName,
                Email = person.Email,
                Department = person.Department,
                LastName = person.LastName,
                LastMeasures = new List<ResearcherLatestMeasureItem>()
            };

            List<Measure> measures = dbContext.Measure.Where(m => m.PersonMeasured == response.Mnumber).ToList();
            foreach(var measure in measures)
            {
                GlobalMeasure globalMeasure = dbContext.GlobalMeasure.Where(m => m.AggregateId == measure.AggregateMeasured).SingleOrDefault();
                if (globalMeasure == default(GlobalMeasure))
                {
                    continue;
                }

                Aggregation aggregation = dbContext.Aggregation.Find(measure.AggregateMeasured);

                Threshold threshold = dbContext.Threshold.Where(t => t.Aggregation == measure.AggregateMeasured)
                        .Where(t => t.ThresholdStart < measure.Value && measure.Value <= t.ThresholdEnd).SingleOrDefault();

                ResearcherLatestMeasureItem researcherLatestMeasure = new ResearcherLatestMeasureItem
                {
                    AggregateId = aggregation.Id.ToString(),
                    AggregateName = aggregation.Name,
                    AggregateType = aggregation.Type,
                    ThresholdId = (threshold != default(Threshold)) ? threshold.Id.ToString() : null,
                    ThresholdName = (threshold != default(Threshold)) ? threshold.ThresholdName : "No matching threshold",
                    DateMeasured = measure.DateMeasured.ToString("yyyy-mm-dd"),
                    value = measure.Value,
                    GlobalMax = globalMeasure.MaximumValue,
                    GlobalMin = globalMeasure.MinimumValue,
                    GlobalMean = globalMeasure.Mean,
                    GlobalMedian = globalMeasure.Median,
                    GlobalStdDev = globalMeasure.StandardDeviation,
                    Id = measure.Id.ToString()
                };
                response.LastMeasures.Add(researcherLatestMeasure);

            }

            return response;
        }

        private bool MatchPersonQuery(Person person, string query)
        {
            query = query.ToLower();
            bool result = true;
            if (!string.IsNullOrEmpty(query))
            {
                bool firstNameMatch = person.FirstName.ToLower().Contains(query);
                bool lastNameMatch = person.LastName.ToLower().Contains(query);
                bool departmentMatch = person.Department.ToLower().Contains(query);
                bool emailMatch = person.Email.ToLower().Contains(query);
                result = firstNameMatch || lastNameMatch || departmentMatch || emailMatch;
            }
            return result;
        }

        private ResearcherProfilerRepository_DEVContext GetDBContext()
        {
            return new ResearcherProfilerRepository_DEVContext();
        }
    }
}
