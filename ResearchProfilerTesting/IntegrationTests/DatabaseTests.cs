using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResearchProfilerRepo.Database;
using ResearchProfilerRepo.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ResearchProfilerTesting.IntegrationTests
{
    [TestClass]
    public class ResearchProfileAggregationTests
    {
        private ResearcherProfilerRepositoryContext dbContext;
        [TestInitialize]
        public void Setup()
        {
            dbContext = new ResearcherProfilerRepositoryContext();
        }
        [TestMethod]
        public void TestSelect()
        {
            List<Aggregation> listAgg = dbContext.Aggregation.ToList();
            Assert.AreEqual(listAgg.Count(), 3);
        }

        [TestMethod]
        public void TestSelectOne()
        {
            Guid aggId = new Guid("d18d456b-6860-475e-96cd-6965d0747855");
            Aggregation agg = dbContext.Aggregation.Find(aggId);
            Assert.AreEqual(agg.Id, aggId);
        }

        [TestMethod]
        public void TestSelectOneWithGlobalMeasure()
        {
            Guid aggId = new Guid("d18d456b-6860-475e-96cd-6965d0747855");
            Aggregation agg = dbContext.Aggregation.Find(aggId);
            var thresholds = dbContext.Entry(agg).Collection(agg => agg.Threshold).Query().ToList();
            Assert.AreEqual(thresholds.Count, 6);
        }

        [TestMethod]
        public void TestCreateUpdateDelete()
        {
            Guid aggId = Guid.NewGuid();
            Aggregation createdAgg = new Aggregation { Id = aggId, Name = "Test Agg Entry", Type = "SUM" };
            dbContext.Aggregation.Add(createdAgg);
            dbContext.SaveChanges();

            Aggregation agg = dbContext.Aggregation.Find(aggId);
            Assert.AreEqual(agg.Id, aggId);
            Assert.AreEqual(agg.Name, createdAgg.Name);
            agg.Name = "BarBar";
            dbContext.Aggregation.Update(agg);
            dbContext.SaveChanges();

            Aggregation updatedAgg = dbContext.Aggregation.Find(aggId);
            dbContext.Aggregation.Find(agg.Id);
            Assert.AreEqual(updatedAgg.Name, "BarBar");

            dbContext.Aggregation.Remove(updatedAgg);
            dbContext.SaveChanges();
            Assert.AreEqual(dbContext.Aggregation.Find(aggId), null);
        }


    }

}
