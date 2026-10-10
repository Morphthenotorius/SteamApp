using Core.Entities.Concrete;
using Core.Utilites.Results;
using Core.Utilites.Results.DataResults;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract
{
    public interface IServiceBase<TGetDTO,TCreateDTO,TUpdateDTO,TEntity>
    {
        Task<IResult> AddAsync(TCreateDTO model);
        Task<IResult> UpdateAsync(TUpdateDTO model,Guid Id);
        Task<IResult> DeleteAsync(Guid id);

        Task<IDataResult<List<TGetDTO>>> GetAsync(
        Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null);

        Task<IDataResult<List<TGetDTO>>> GetAllAsync();
        Task<IDataResult<TGetDTO>> GetByIdAsync(Guid Id);
        Task<IDataResult<PagedResult<TGetDTO>>> GetPagedAsync(Expression<Func<TEntity,bool>>? filter,Func<IQueryable<TEntity>,IQueryable<TEntity>>? include,Func<List<TGetDTO>,IEnumerable<TGetDTO>>? sort, int pageIndex,int pageSize);
    }
}
