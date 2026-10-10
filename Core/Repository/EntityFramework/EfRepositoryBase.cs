using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.Repository.EntityFramework
{
    public class EfRepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext, new()
    {
        protected readonly TContext _context;
        
        public EfRepositoryBase(TContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null, bool tracking = false)
        {
            IQueryable<TEntity> result = _context.Set<TEntity>();
            if (!tracking)
            {
                result = result.AsNoTracking();
            }

            if(expression != null)
            {
                result = result.Where(expression);
            }

            return await result.ToListAsync();
        }

            public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null)
            {
                IQueryable<TEntity> result = _context.Set<TEntity>();

                if(include != null)
                {
                    result = include(result);
                }

                if(expression!= null)
                {
                    result = result.Where(expression);
                }

                return await result.ToListAsync();
            }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task RemoveAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
