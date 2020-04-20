using ResearchProfilerRepo.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ResearchProfilerRepo.Database.Repositories
{
    public class GlobalMeasureRepo : BaseResearcherRepo<GlobalMeasure, Guid>
    {
        public override List<GlobalMeasure> GetAll()
        {
            throw new NotImplementedException();
        }

        public override List<GlobalMeasure> GetAllWhere(Expression<Func<GlobalMeasure, bool>> condition)
        {
            throw new NotImplementedException();
        }
    }
}
