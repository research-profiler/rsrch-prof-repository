using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ResearchProfilerRepo.Database.Models;

namespace ResearchProfilerRepo.Database.Repositories
{
    public abstract class BaseResearcherRepo<T, IDType> where T : class, IModel<IDType>
    {
        protected ResearcherProfilerRepositoryContext dbContext;
        private readonly Type T_TYPE = typeof(T);

        public BaseResearcherRepo()
        {
            Initialize(new ResearcherProfilerRepositoryContext());
        }

        public BaseResearcherRepo(ResearcherProfilerRepositoryContext databaseContext)
        {
            Initialize(databaseContext);
        }

        private void Initialize(ResearcherProfilerRepositoryContext databaseContext)
        {
            dbContext = databaseContext;
        }

        /// <summary>
        /// Returns one of T based on the primary key defined in the definition of the Object
        /// </summary>
        /// <param name="id">The primary key of the item</param>
        /// <returns>The item requested</returns>
        public T GetOne(IDType id)
        {
            T entity = dbContext.Find(T_TYPE, id) as T;
            return entity;
        }

        /// <summary>
        /// Returns the object identified by the primary key of the stub object
        /// </summary>
        /// <param name="stubObject">Any object which has at least enough information to allow GetPrimaryKey to succeed</param>
        /// <returns>The full version of the object passed</returns>
        public T GetOne(T stubObject)
        {
            return GetOne(stubObject.GetPrimaryKey());
        }

        /// <summary>
        /// Inserts the passed entity into the database
        /// </summary>
        /// <param name="entity">The entity to be inserted</param>
        /// <returns></returns>
        public T Insert(T entity)
        {
            dbContext.Add(entity);
            dbContext.SaveChanges();
            T insertedRecord = this.GetOne(entity);
            return insertedRecord;
        }

        /// <summary>
        /// Updates the passed entity and then return the updated entity
        /// </summary>
        /// <param name="entity">The object to be updated</param>
        /// <returns>The object having been updated</returns>
        public T Update(T entity)
        {
            dbContext.Update(entity);
            dbContext.SaveChanges();
            return entity;
        }

        /// <summary>
        /// Retrieves all of the item from the database
        /// </summary>
        /// <returns>A list containing all of the items from the database</returns>
        public abstract List<T> GetAll();

        /// <summary>
        /// Returns all of the objects where the passed condition
        /// 
        /// Will return an empty list if the condition is not met in the database
        /// </summary>
        /// <param name="condition">A lync expression that the results should match</param>
        /// <returns>The list of all objects which match the condition.</returns>
        public abstract List<T> GetAllWhere(Expression<Func<T, bool>> condition);

        /// <summary>
        /// Deletes the passed Object
        /// </summary>
        /// <param name="obj">The object to be deleted</param>
        public void Delete(T obj)
        {
            dbContext.Remove(obj);

            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
