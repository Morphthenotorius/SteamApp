using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs.CompanyDTO
{
    public sealed record GetCompanyDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string WebsiteUrl {  get; set; }
    }
}
