using Business.DTOs.CompanyDTO;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICompanyService : IServiceBase<GetCompanyDTO,CreateCompanyDTO,UpdateCompanyDTO,Company>
    {
    }
}
