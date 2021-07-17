using AutoMapper;
using CleanApplication.Application.Common.Interfaces;
using CleanApplication.Application.Common.Models;
using CleanApplication.Application.Dto;
using CleanApplication.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanApplication.Application.Guests.Commands.GetGuestToken
{
    public class GetGuestTokenCommand : IRequestWrapper<GuestLoginResponseDto>
    {
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SmsCode { get; set; }

    }
    public class GetGuestTokenCommandHandler : IRequestHandlerWrapper<GetGuestTokenCommand, GuestLoginResponseDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IIdentityService _identityService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICacheService _CacheService;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public GetGuestTokenCommandHandler(IApplicationDbContext context, IMapper mapper,
            IIdentityService identityService, ICacheService CacheService,
            ITokenService tokenService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _identityService = identityService;
            _CacheService = CacheService;
            _tokenService = tokenService;
            _userManager = userManager;
        }
        public async Task<ServiceResult<GuestLoginResponseDto>> Handle(GetGuestTokenCommand request, CancellationToken cancellationToken)
        {

            ApplicationUser entity = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber, cancellationToken);
            if (entity != null)
            {
                if(!await _userManager.IsInRoleAsync(entity, "guest"))
                    return ServiceResult.Failed<GuestLoginResponseDto>(ServiceError.InvalidUserGuestRole);


                double remaining = (DateTime.Now - entity.Created).TotalDays;
                var isExpire = remaining > 20;
                if (isExpire)
                {
                    return ServiceResult.Failed<GuestLoginResponseDto>(ServiceError.GuestUserInvalidDate);
                }
                var userDto= _mapper.Map<GuestLoginResponseDto>(entity);
                userDto.Token=_identityService.CreateJwtToken(userDto.Id.ToString(), DateTime.UtcNow.AddDays(remaining));
                return ServiceResult.Success(userDto);
                

            }


            entity = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                UserName = request.PhoneNumber,
                Email = $"guest{request.PhoneNumber}@debanjhgroup.com",
                Created = DateTime.Now
            };
            var newUser = await _identityService.RegisterGuest(entity);
            return ServiceResult.Success(newUser);

        }

        private GuestLoginResponseDto returnSuccess(ApplicationUser user)
        {
            var result = _mapper.Map<GuestLoginResponseDto>(user);
            result.Token = _identityService.CreateJwtToken(result.Id.ToString(), DateTime.UtcNow.AddDays(20));
            return result;

        }

    }
}
