using Business.DTOs.CategoryDTO;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICategoryService : IServiceBase<GetCategoryDTO,CreateCategoryDTO,UpdateCategoryDTO,Category>
    {
    }
}
