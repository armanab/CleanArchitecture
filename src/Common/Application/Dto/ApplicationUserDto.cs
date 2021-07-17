using CleanApplication.Application.Common.Mappings;
using CleanApplication.Domain.Entities;
using System;

namespace CleanApplication.Application.Dto
{
    public class ApplicationUserDto : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailConfirmed { get; set; }
        public string PhoneNumberConfirmed { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public bool IsActive { get; set; }


    }
}
