using CleanApplication.Application.Common.Mappings;
using CleanApplication.Domain.Entities;

namespace CleanApplication.Application.Dto
{
    public class UserCreateDto : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool EmailSent { get; set; }

    }
}
