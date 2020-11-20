using GroupClass.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Core.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string UserRole { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string ActivationCode { get; set; }
        public string ProfileImageUrl { get; set; }
        public string SchoolNumber { get; set; }

        public UserDto()
        {

        }
        public UserDto(User model)
        {
            this.Id = model.Id;
            this.CreatedAt = model.CreatedAt;
            this.ModifiedAt = model.ModifiedAt;
            this.UserRole = model.UserRole;
            this.Firstname = model.Firstname;
            this.Lastname = model.Lastname;
            this.Email = model.Email;
            //this.ActivationCode = model.ActivationCode;
            this.ProfileImageUrl = model.ProfileImageUrl;
            this.SchoolNumber = model.SchoolNumber;
        }
    }
}
