using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.User
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName {  get; set; }
        public string LastName {  get; set; }
        public string? RefreshToken {  get; set; }
        public DateTime? RefreshTokenExpiration {  get; set; }
        public decimal Balance {  get; set; }
    }
}
