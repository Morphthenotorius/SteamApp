using Business.Abstract;
using Core.Entities.Abstract;
using Core.Repository;
using Core.Utilites.Results;
using Core.Utilites.Results.DataResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Business.Concrete
{
    public class BaseManager<TEntity, TGetDTO, TCreateDTO, TUpdateDTO> : IServiceBase<TGetDTO, TCreateDTO, TUpdateDTO,TEntity>
        where TEntity : class, IEntity, new()
    {

        private readonly IRepositoryBase<TEntity> _repository;
        private readonly Func<TCreateDTO, TEntity> _mapToCreateDTO;
        protected readonly Func<TEntity, TGetDTO> _mapToGetDTO;
        private readonly Action<TUpdateDTO, TEntity> _mapToUpdateDTO;

        public BaseManager(IRepositoryBase<TEntity> repository, 
            Func<TCreateDTO, TEntity> mapToCreateDTO, Func<TEntity,TGetDTO > mapToGetDTO, Action<TUpdateDTO, TEntity> mapToUpdateDTO)
        {                                                      
            _repository = repository;                          
            _mapToCreateDTO = mapToCreateDTO;
            _mapToGetDTO = mapToGetDTO;
            _mapToUpdateDTO = mapToUpdateDTO;
        }


        public virtual async Task<IResult> AddAsync(TCreateDTO model)
        {
            try
            {
                if(model == null)
                {
                    return new ErrorResult("Data Cannot Be Null!");
                }
                var entity = _mapToCreateDTO(model);
                await _repository.AddAsync(entity);
                return new SuccessResult("Successfully Added");
            }
            
            catch (Exception ex)
            {
                return new ErrorResult($"There is an error occured during add process: {ex.Message}");
            }
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if(entity == null)
                {
                    return new ErrorResult("Data Cannot Be Null!");
                }

                await _repository.RemoveAsync(entity);
                return new SuccessResult("The data has been deleted successfully");
            }
            catch(Exception ex)
            {
                return new ErrorResult($"There is an error occured during delete process: {ex.Message}");
            }
        }

        public virtual async Task<IDataResult<List<TGetDTO>>> GetAllAsync()
        {
            try
            {
                var entites = await _repository.GetAllAsync();
                if(entites == null)
                {
                    return new ErrorDataResult <List<TGetDTO>>("Upcoming data cannot be null");
                }
                var dtoList = entites.Select(x => _mapToGetDTO(x)).ToList();
                return new SuccessDataResult<List<TGetDTO>>(dtoList,"Datas have been provided");
            }

            catch(Exception ex) 
            {
                return new ErrorDataResult<List<TGetDTO>>($"There is an error occured during get process: {ex.Message}");
            }
        }

        public virtual async Task<IDataResult<TGetDTO>> GetByIdAsync(Guid Id)
        {
            try
            {
                if(Id == default)
                {
                    return new ErrorDataResult<TGetDTO>("Incorret Id please make sure that you have entered the right Id!");
                }
                var entity = await _repository.GetByIdAsync(Id);
                if(entity == null)
                {
                    return new ErrorDataResult<TGetDTO>("Data due to that id could not found");
                }
                var result = _mapToGetDTO(entity);
                return new SuccessDataResult<TGetDTO>(result,"Data provided successfully");
            }
            catch (Exception ex) 
            {
                return new ErrorDataResult<TGetDTO>($"There is an error occured during data retrieve process!{ex.Message}");
            }
        }

        public async Task<IResult> UpdateAsync(TUpdateDTO model,Guid Id)
        {
            try
            {
                if(model == null)
                {
                    return new ErrorResult("Object cannot be null");
                }
                var entity = await _repository.GetByIdAsync(Id);
                if(entity == null)
                {
                    return new ErrorResult("Object cannot be found");
                }
                _mapToUpdateDTO(model, entity);
                await _repository.UpdateAsync(entity);
                return new SuccessResult("Object successfully updated");
            }
            catch(Exception ex)
            {
                return new ErrorResult($"There is an error occured during update process!{ex.Message}");
            }
        }

       public async Task<IDataResult<TGetDTO>> GetAsync(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IQueryable<TEntity>>? include)
        {
            try
            {
                var entity = await _repository.GetAsync(expression, include);
                if(entity == null)
                {
                    return new ErrorDataResult<TGetDTO>("Upcoming data cannot be null");
                }
                var dto = _mapToGetDTO(entity);
                return new SuccessDataResult<TGetDTO>(dto,"Data has been provided successfully");
            }

            catch (Exception ex) 
            {
                return new ErrorDataResult<TGetDTO>($"An Error Occured! {ex.Message}");
            }
        }
    }
}
