using CleanApplication.Application.Common.Enum;
using CleanApplication.Application.Common.Models;
using CleanApplication.Application.Dto;
using CleanApplication.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanApplication.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(Guid userId);
        Task<bool> ExistUserByPhoneNumberAsync(string phoneNumber);

        Task<bool> IsInRoleAsync(Guid userId, string role);

        Task<bool> AuthorizeAsync(Guid userId, string policyName);

        Task<(Result Result, Guid UserId)> CreateUserAsync(string userName, string password);   

        Task<ServiceResult<bool>> DeleteUserAsync(Guid userId);
        Task<ServiceResult<LoginResponseDto>> SignIn(string userName,string password, string ipAddress);
        Task<ServiceResult<UserCreateDto>> Register(ApplicationUser User, ApplicationRoleEnum Role=ApplicationRoleEnum.User);
        Task<GuestLoginResponseDto> RegisterGuest(ApplicationUser User);

        Task<ServiceResult<bool>> ChangePasswordAsync(string email,string phoneNumber, string password);

        string CreateJwtToken(string data, DateTime expire);

        Task<bool> ExistUserByEmailAsync(string phoneNumber);

        Task<RefreshTokenResponseDto> RefreshToken(string token, string ipAddress);
        Task<RevokeTokenResponseDto> RevokeToken(string token, string ipAddress);
        Task<ApplicationUser> GetUserByIdAsync(Guid Id, CancellationToken cancellationToken);
        Task<ServiceResult<bool>> Update(ApplicationUser User);

    }
}
    