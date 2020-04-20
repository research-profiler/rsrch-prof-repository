using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResearchProfilerRepo.Database;
using ResearchProfilerRepo.Database.Models;
using ResearchProfilerRepo.Database.Repositories;

namespace ResearchProfilerTesting.UnitTests.Database.Repositories
{
    /// <summary>
    /// Test class, utilizes the aggregation table
    /// </summary>
    internal class TestAggregateRepo : BaseResearcherRepo<Aggregation, Guid>
    {
        public override List<Aggregation> GetAll()
        {
            throw new NotImplementedException();
        }

        public override List<Aggregation> GetAllWhere(IQueryable<Aggregation> condition)
        {
            throw new NotImplementedException();
        }
    }

    [TestClass]
    public class TestBaseResearcherRepo
    {
        private TestAggregateRepo repo;
        private ResearcherProfilerRepositoryContext dbContext;
        private Guid testId;

        [TestInitialize]
        public void Setup()
        {
            repo = new TestAggregateRepo();
            dbContext = new ResearcherProfilerRepositoryContext();
            testId = Guid.NewGuid();
        }

        [TestMethod]
        public void TestGetOneByGuid()
        {
            Guid aggId = new Guid("d18d456b-6860-475e-96cd-6965d0747855");
            Aggregation aggregation = repo.GetOne(aggId);
            Assert.AreEqual(aggregation.Id, aggId);
        }

        [TestMethod]
        public void TestGetOneByStub()
        {

            Guid aggId = new Guid("d18d456b-6860-475e-96cd-6965d0747855");
            Aggregation stub = new Aggregation { Id = aggId };
            Aggregation aggregation = repo.GetOne(stub);
            Assert.AreEqual(aggregation.Id, aggId);
        }

        [TestMethod]
        public void TestCreate()
        {
            Aggregation createdAgg = new Aggregation { Id = testId, Name = "Test Agg Entry", Type = "SUM" };
            Aggregation aggregation = repo.Insert(createdAgg);

            Aggregation agg = dbContext.Aggregation.Find(testId);
            Assert.AreEqual(agg.Id, testId);

            // Cleanup the inserted record
            dbContext.Aggregation.Remove(agg);
            dbContext.SaveChanges();
        }

        [TestMethod]
        public void TestUpdate()
        {            
            Aggregation createdAgg = new Aggregation { Id = testId, Name = "Test Agg Entry", Type = "SUM" };
            dbContext.Aggregation.Add(createdAgg);
            dbContext.SaveChanges();
            createdAgg.Name = "Entry";

            dbContext.Dispose();
            repo.Update(createdAgg);
            dbContext = new ResearcherProfilerRepositoryContext();

            // Retrieve record from database
            Aggregation updatedAgg = dbContext.Aggregation.Find(testId);
            dbContext.Aggregation.Find(testId);
            Assert.AreEqual(updatedAgg.Id, testId);
            Assert.AreEqual(updatedAgg.Name, "Entry");

            // Cleanup the inserted record
            dbContext.Aggregation.Remove(updatedAgg);
            dbContext.SaveChanges();
        }

        [TestMethod]
        public void TestDelete()
        {
            Aggregation createdAgg = new Aggregation { Id = testId, Name = "Test Agg Entry", Type = "SUM" };
            dbContext.Aggregation.Add(createdAgg);
            dbContext.SaveChanges();

            dbContext.Dispose();
            repo.Delete(createdAgg);
            dbContext = new ResearcherProfilerRepositoryContext();

            Aggregation result = dbContext.Aggregation.Find(testId);
            Assert.AreEqual(result, null);
        }

        [TestCleanup]
        public void TestTeardown()
        {
            var result = dbContext.Aggregation.Find(testId);

            if (result != null)
            {
                dbContext.Aggregation.Remove(result);
                dbContext.SaveChanges();
            }
        }
    }
}
