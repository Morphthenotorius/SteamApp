using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class User : BaseEntity,IEntity
    {
        public string Username {  get; set; }
        public string Email {  get; set; }
        public byte[] PasswordHash{  get; set; }
        public Library Library { get; set; }

        //Profile 
        public string? ProfilePictureUrl {  get; set; }
        public string? Bio {  get; set; }
        public decimal WalletBalance {  get; set; }


        
    }
}
