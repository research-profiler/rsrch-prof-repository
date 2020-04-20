using ResearchProfilerRepo.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ResearchProfilerRepo.Database.Repositories
{
    public class ThresholdRepo : BaseResearcherRepo<Threshold, Guid>
    {
        public override List<Threshold> GetAll()
        {
            throw new NotImplementedException();
        }

        public override List<Threshold> GetAllWhere(Expression<Func<Threshold, bool>> condition)
        {
            throw new NotImplementedException();
        }
    }
}
