using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.Repository
{
    public interface IRepositoryBase <TEntity>
        where TEntity : class,IEntity
    {
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);

        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression,Func<IQueryable<TEntity> , IQueryable<TEntity>>? include = null);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null, bool tracking = false);
        Task<TEntity> GetByIdAsync(Guid id);
    }
}
