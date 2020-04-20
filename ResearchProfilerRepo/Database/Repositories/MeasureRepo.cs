using ResearchProfilerRepo.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ResearchProfilerRepo.Database.Repositories
{
    public class MeasureRepo : BaseResearcherRepo<Measure, Guid>
    {
        public override List<Measure> GetAll()
        {
            throw new NotImplementedException();
        }

        public override List<Measure> GetAllWhere(Expression<Func<Measure, bool>> condition)
        {
            throw new NotImplementedException();
        }
    }
}
