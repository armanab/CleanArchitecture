using AutoMapper;
using CleanApplication.Application.Common.Interfaces;
using CleanApplication.Application.Common.Models;
using CleanApplication.Application.Dto;
using CleanApplication.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanApplication.Application.Users.Commands.Create
{
    public class CreateUserCommand : IRequestWrapper<UserCreateDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int Gender { get; set; }
        public string SmsCode { get; set; }
    }
    public class CreateUserCommandHandler : IRequestHandlerWrapper<CreateUserCommand, UserCreateDto>
    {
        private readonly IMapper _mapper;
        //private readonly IDistributedCache _redisCache;
        private readonly ICacheService _CacheService;
        IIdentityService _identityService;

        public CreateUserCommandHandler(IMapper mapper, IIdentityService identityService, ICacheService cacheService)
        {
            _mapper = mapper;
            _identityService = identityService;
            _CacheService = cacheService;

        }
        public async Task<ServiceResult<UserCreateDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var cacheCodeString = _CacheService.Get<string>(string.Format(Constants.RegisterCode_CreateUser, request.PhoneNumber));
            if (request.SmsCode != cacheCodeString)
            {
                //return BadRequest(new { code = "InvalidConfirmVerifyCode", message = "کد تاییدیه شماره تلفن معتبر نمی باشد." });
                return ServiceResult.Failed<UserCreateDto>(ServiceError.InvalidVerifyCode);
            }

            ApplicationUser appUser = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                DateOfBirth = request.DateOfBirth,
                PasswordHash = request.Password,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Gender = request.Gender,
                PhoneNumberConfirmed = true,
                IsActive = true
            };
            return await _identityService.Register(appUser);
        }
    }
}
