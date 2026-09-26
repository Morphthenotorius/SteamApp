using Business.Abstract;
using Business.DTOs.CompanyDTO;
using Core.Entities.Concrete;
using Core.Repository;
using DataAccess.Abstract;
using DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CompanyManager : BaseManager<Company,GetCompanyDTO,CreateCompanyDTO,UpdateCompanyDTO>, ICompanyService
    {
        public CompanyManager(ICompanyDAL companyDAL) : base(companyDAL,
        createDTO => new Company
        {
            Name = createDTO.Name,
            WebsiteUrl = createDTO.WebsiteUrl
        },
            static entity => new GetCompanyDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                WebsiteUrl = entity.WebsiteUrl
            },
            (updatedDto, existingEntity) =>
            {
                existingEntity.Name = updatedDto.Name;
                existingEntity.WebsiteUrl = updatedDto.WebsiteUrl; 
            } 
            )
        {

        }

    }
}
