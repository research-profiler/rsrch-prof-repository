using ResearchProfilerRepo.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ResearchProfilerRepo.Database.Repositories
{
    public class PersonRepo : BaseResearcherRepo<Person, string>
    {
        public override List<Person> GetAll()
        {
            throw new NotImplementedException();
        }

        public override List<Person> GetAllWhere(Expression<Func<Person, bool>> condition)
        {
            throw new NotImplementedException();
        }
    }
}
