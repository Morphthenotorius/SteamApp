using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs.CompanyDTO
{
    public sealed record CreateCompanyDTO
    {
        public required string Name {  get; set; }
        public string? WebsiteUrl {  get; set; }
    }
}
