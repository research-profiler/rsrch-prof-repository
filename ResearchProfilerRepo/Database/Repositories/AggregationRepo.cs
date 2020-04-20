using ResearchProfilerRepo.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ResearchProfilerRepo.Database.Repositories
{
    public class AggregationRepo : BaseResearcherRepo<Aggregation, Guid>
    {
        public override List<Aggregation> GetAll()
        {
            return dbContext.Aggregation.ToList();
        }

        public override List<Aggregation> GetAllWhere(Expression<Func<Aggregation, bool>> condition)
        {
            List<Aggregation> matchingAggregations = dbContext.Aggregation.Where(condition).ToList();
            return matchingAggregations;
        }
        public List<Measure> GetCurrentMeasures(Aggregation aggregation)
        {
            return null;
        }
        public List<Measure> GetMeasuresFor(Aggregation aggregate)
        {
            throw new NotImplementedException();
        } 

        protected List<Threshold> GetThresholds(Aggregation aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
