using Business.Abstract;
using Business.DTOs.CategoryDTO;
using Core.Entities.Concrete;
using Core.Repository;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CategoryManager : BaseManager<Category, GetCategoryDTO, CreateCategoryDTO, UpdateCategoryDTO>, ICategoryService
    {
        public CategoryManager(ICategoryDAL _categoryDAL) : base(_categoryDAL,
        
            createDTO => new Category
            {
                Name = createDTO.Name
            },

           static entity => new GetCategoryDTO
            {
                Id = entity.Id,
                Name = entity.Name,
            },

            (updateDTO,existingCategory) =>
            {
                existingCategory.Name = updateDTO.Name;
            }
            )
            
        {

        }
    }
}
