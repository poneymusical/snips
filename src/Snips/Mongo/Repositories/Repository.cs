using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Snips.Domain.BusinessObjects.Base;
using Snips.Domain.Interfaces;

namespace Snips.Mongo.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : IIdentifiable
    {
        protected readonly IMongoCollection<TEntity> Collection;
        protected readonly IMongoDatabase Database;

        public Repository(MongoSettings settings, string collectionName)
        {
            var client = new MongoClient(settings.ConnectionString);
            Database = client.GetDatabase(settings.DatabaseName);
            Collection = Database.GetCollection<TEntity>(collectionName);
        }
        
        public async Task<IList<TEntity>> GetAll()
        {
            var entities = await Collection.AsQueryable().ToListAsync();
            return entities;
        }

        public async Task<IList<TEntity>> Get(Expression<Func<TEntity, bool>> criteria)
        {
            var entities = await Collection.AsQueryable().Where(criteria).ToListAsync();
            return entities;
        }

        public async Task<TEntity> GetSingle(Guid id)
        {
            var entity = await Collection.AsQueryable().Where(e => e.Id == id).FirstOrDefaultAsync();
            return entity;
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            await Collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task Update(TEntity entity)
        {
            await Collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);
        }

        public async Task Delete(TEntity entity)
        {
            await Collection.DeleteOneAsync(e => e.Id == entity.Id);
        }
    }
}