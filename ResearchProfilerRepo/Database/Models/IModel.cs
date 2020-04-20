using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchProfilerRepo.Database.Models
{
    public interface IModel<IDType>
    {
        public IDType GetPrimaryKey();
    }
}
