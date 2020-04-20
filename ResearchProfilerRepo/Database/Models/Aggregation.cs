using ResearchProfilerRepo.Database.Repositories;
using System;
using System.Collections.Generic;

namespace ResearchProfilerRepo.Database.Models
{
    public partial class Aggregation : IModel<Guid>
    {
        public Aggregation()
        {
            GlobalMeasure = new HashSet<GlobalMeasure>();
            InverseParentNameNavigation = new HashSet<Aggregation>();
            Measure = new HashSet<Measure>();
            Threshold = new HashSet<Threshold>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Guid ParentName { get; private set; }

        private Aggregation _parent = null;
        public Aggregation Parent
        {
            get
            {
                return GetParent();
            }
            set
            {
                SetParent(value);
            }
        }

        public Aggregation GetParent()
        {
            if (_parent == null)
            {
                AggregationRepo repo = new AggregationRepo();
                _parent = repo.GetOne(ParentName);
                if (_parent == null)
                {
                    throw new ParentNotFoundException();
                }
                repo.Dispose();
            }
            return _parent;
        }

        public void SetParent(Aggregation value)
        {
            AggregationRepo repo = new AggregationRepo();
            this.ParentName = value.Id;

        }


        public Guid GetPrimaryKey()
        {
            return Id;
        }


        public virtual Aggregation ParentNameNavigation { get; set; }
        public virtual ICollection<GlobalMeasure> GlobalMeasure { get; set; }
        public virtual ICollection<Aggregation> InverseParentNameNavigation { get; set; }
        public virtual ICollection<Measure> Measure { get; set; }
        public virtual ICollection<Threshold> Threshold { get; set; }
    }

    class ParentNotFoundException : Exception { }
}
