using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Snips.Domain.BusinessObjects.Base;

namespace Snips.Domain.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : IIdentifiable
    {
        Task<IList<TEntity>> GetAll();
        Task<IList<TEntity>> Get(Expression<Func<TEntity, bool>> criteria);
        Task<TEntity> GetSingle(Guid id);
        Task<TEntity> Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
    }
}