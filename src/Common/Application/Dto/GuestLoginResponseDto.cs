

using AutoMapper;
using CleanApplication.Application.Common.Mappings;
using CleanApplication.Domain.Entities;
using System;

namespace CleanApplication.Application.Dto
{
    public class GuestLoginResponseDto : IMapFrom<ApplicationUser>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public DateTime Created { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationUser, GuestLoginResponseDto>()
                .ForMember(d => d.Created, opt => opt.MapFrom(s => s.Created.ToString("yyyy/MM/dd")));
        }
    }
}
