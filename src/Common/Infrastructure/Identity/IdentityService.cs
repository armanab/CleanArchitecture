using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanApplication.Application.Common.Enum;
using CleanApplication.Application.Common.Exceptions;
using CleanApplication.Application.Common.Interfaces;
using CleanApplication.Application.Common.Models;
using CleanApplication.Application.Dto;
using CleanApplication.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace CleanApplication.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWT _appSettings;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly EmailApp _emailApp;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _context;
        private readonly IStringUtilities _stringUtilities;
    


        public IdentityService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<JWT> appSettings,
             IOptions<EmailApp> emailApp,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService,
            ITokenService tokenService,
            IEmailService emailService,
            IConfiguration configuration,
            IStringUtilities stringUtilities,
            IHttpContextAccessor context,
            IMapper mapper,
            ILogger<IdentityService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
            _appSettings = appSettings.Value;
            _tokenService = tokenService;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
            _emailService = emailService;
            _context = context;
            _emailApp = emailApp.Value;
            _stringUtilities = stringUtilities;




        }

        public async Task<string> GetUserNameAsync(Guid userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public async Task<(Result Result, Guid UserId)> CreateUserAsync(string userName, string password)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = userName,
            };

            var result = await _userManager.CreateAsync(user, password);

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<bool> IsInRoleAsync(Guid userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> AuthorizeAsync(Guid userId, string policyName)
        {
            ApplicationUser user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

            var result = await _authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }

        public async Task<ServiceResult<LoginResponseDto>> SignIn(string userName, string password, string ipAddress)
        {

            var result = await _signInManager.PasswordSignInAsync(userName, password, false, lockoutOnFailure: true);


        
            if (result.IsLockedOut)
            {
                return ServiceResult.Failed<LoginResponseDto>(ServiceError.UserIsLockedOut);
            }
            else if (!result.Succeeded)
            {
                return ServiceResult.Failed<LoginResponseDto>(ServiceError.InccrrectUsernameOrPassword);

            }
            var user = await _userManager.Users.Include(m => m.RefreshTokens).FirstOrDefaultAsync(x => x.UserName == userName);
            var expireTime = DateTime.UtcNow.AddDays(7);
            RefreshToken refreshToken = generateRefreshToken(ipAddress, expireTime);
            user.RefreshTokens.Add(refreshToken);
            var updateUser = await _userManager.UpdateAsync(user);

            if (!updateUser.Succeeded)
            {
                return ServiceResult.Failed<LoginResponseDto>(ServiceError.ErrorInSaveOrUpdate);

            }
            return ServiceResult.Success(new LoginResponseDto
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                Token = CreateJwtToken(user.Id.ToString(), expireTime),
                RefreshToken = refreshToken.Token,
                Expire = expireTime
            });

        }

        public async Task<ServiceResult<UserCreateDto>> Register(ApplicationUser User, ApplicationRoleEnum Role = ApplicationRoleEnum.User)
        {
            Expression<Func<ApplicationUser, bool>> _predicateBuilder = c => true;
            _predicateBuilder = c => c.Email == User.Email && c.PhoneNumber == User.PhoneNumber;
            return await RegisterCore(User, _predicateBuilder, true, Role.ToString());

        }
        public async Task<GuestLoginResponseDto> RegisterGuest(ApplicationUser User)
        {
            GuestLoginResponseDto userDto = null;
            var pass = _stringUtilities.MD5Hash(User.PhoneNumber);
            IdentityResult result = await _userManager.CreateAsync(User, pass);
            if (result.Succeeded)
            {
                _logger.LogInformation("user Registered", User.UserName);
                var userRole = new ApplicationRole("Guest");
                await _userManager.AddToRolesAsync(User, new[] { userRole.Name });
                userDto = _mapper.Map<GuestLoginResponseDto>(User);
                userDto.Token = CreateJwtToken(userDto.Id.ToString(), DateTime.UtcNow.AddDays(20));
            }
            return userDto;
        }
        private async Task<ServiceResult<UserCreateDto>> RegisterCore(ApplicationUser User, Expression<Func<ApplicationUser, bool>> predicateFilterUser,
            bool sendEmail, string role = "User")
        {

            var user = await _userManager.Users.FirstOrDefaultAsync(predicateFilterUser);
            if (user != null)
            {
                ServiceResult.Failed<UserCreateDto>(ServiceError.DuplicatePhoneNumberOrEmail);
            }
            var userRole = new ApplicationRole(role);
            UserCreateDto userDto = null;
            var pass = User.PasswordHash;
            IdentityResult result = await _userManager.CreateAsync(User, pass);
            await _userManager.AddToRolesAsync(User, new[] { userRole.Name });
            if (result.Succeeded)
            {
                _logger.LogInformation("user Registered", User.UserName);
                await _userManager.AddToRolesAsync(User, new[] { userRole.Name });

                userDto = _mapper.Map<UserCreateDto>(User);
                if (sendEmail)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(User);
                    userDto.EmailSent = SendConfirmEmail(User, token);

                }

            }
            else
            {

                return ServiceResult.Failed<UserCreateDto>(new ServiceError(result.Errors?.FirstOrDefault().Description, 1));
                //ServiceResult.Failed<UserCreateDto>(ServiceError.CreateUserException);

            }
            return ServiceResult.Success(userDto);

        }
        private bool SendConfirmEmail(ApplicationUser User, string token)
        {
            string confirmationLink = $"{ _context.HttpContext.Request.Scheme}://{ _context.HttpContext.Request.Host}/api/Authenticate/ConfirmEmail?token={System.Web.HttpUtility.UrlEncode(token)}&email={User.Email}";
           
            _emailApp.To = User.Email;
            _emailApp.Body = "لطفاً با کلیک روی این پیوند حساب خود را تأیید کنید: <a href=\""
                                    + confirmationLink + "\">link</a>";
            _emailApp.IsBodyHtml = true;
            _emailApp.Subject = "تاییدیه حساب کاربری";

            var EmailSent = _emailService.SendEmail(_emailApp);
            if (!EmailSent)
            {
                // log email failed 
                _logger.LogError("CleanApplication send email: email failed to  {username}", User.UserName);
                //return BadRequest(new { message = "email failed sent." });
            }
            return EmailSent;
        }
  

        public async Task<ServiceResult<bool>> DeleteUserAsync(Guid userId)
        {
            ApplicationUser user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
            {
                throw new NotFoundException(nameof(ApplicationUserDto), userId);

            }
            var result = await DeleteUserAsync(user);


            return result.Succeeded ? ServiceResult.Success(true) : ServiceResult.Failed<bool>(new ServiceError(string.Join(',', result.Errors), 1));
        }

        public async Task<IdentityResult> DeleteUserAsync(ApplicationUser user)
        {
            //IdentityResult result = await _userManager.DeleteAsync(user);
            user.IsDeleted = true;
            return await _userManager.UpdateAsync(user);



            //return result;
        }

        public async Task<ServiceResult<bool>> ChangePasswordAsync(string email, string phoneNumber, string password)
        {
            ApplicationUser user = _userManager.Users.SingleOrDefault(u => u.Email == email && u.PhoneNumber == phoneNumber);

            if (user == null)
            {
                return ServiceResult.Failed<bool>(ServiceError.UserNotFound);
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, password);
            if (!result.Succeeded)
            {
                return ServiceResult.Failed<bool>(new ServiceError(string.Join(",", result.Errors.Select(x => x.Description)), 2));
            }

            return ServiceResult.Success(true);
        }


        public async Task<bool> ExistUserByPhoneNumberAsync(string phoneNumber)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber) != null;


        }
        public async Task<bool> ExistUserByEmailAsync(string email)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email) != null;


        }
        public async Task<ApplicationUser> GetUserByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _userManager.Users.Where(u => u.Id == Id)
          .FirstOrDefaultAsync(cancellationToken);
        }

    
        public string CreateJwtToken(string data, DateTime expire)
        {
            return _tokenService.CreateJwtSecurityToken(data, _configuration["JWT:Secret"], expire);
        }

        public async Task<RefreshTokenResponseDto> RefreshToken(string token, string ipAddress)
        {
            var user = _userManager.Users.Include(m => m.RefreshTokens).SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

            // return null if no user found with token
            if (user == null) return null;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return null if token is no longer active
            if (!refreshToken.IsActive) return null;

            // replace old refresh token with a new one and save
            var newRefreshToken = generateRefreshToken(ipAddress, DateTime.UtcNow.AddDays(7));
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            user.RefreshTokens.Add(newRefreshToken);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return null;
            }
            // generate new jwt
            var jwtToken = CreateJwtToken(user.Id.ToString(), DateTime.UtcNow.AddDays(7));

            return new RefreshTokenResponseDto(user, jwtToken, newRefreshToken.Token);
        }

        public async Task<RevokeTokenResponseDto> RevokeToken(string token, string ipAddress)
        {
            var user = _userManager.Users.Include(m => m.RefreshTokens).SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

            // return false if no user found with token
            if (user == null) return null;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return false if token is not active
            if (!refreshToken.IsActive) return null;

            // revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return null;
            }
            return new RevokeTokenResponseDto(true, "توکن با موفقیت لغو شد");
        }
        private RefreshToken generateRefreshToken(string ipAddress, DateTime expire)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = expire,
                    Created = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };
            }
        }
        public async Task<ServiceResult<bool>> Update(ApplicationUser User)
        {
            var _user = _userManager.Users.FirstOrDefault(x => x.Id == User.Id && x.Email != "administrator@localhost.com");
            if (_user == null)
            {
                throw new NotFoundException(nameof(ApplicationUser), User.Email);

            }

            _user.DateOfBirth = User.DateOfBirth;
            _user.FirstName = User.FirstName;
            _user.LastName = User.LastName;
            _user.IsActive = User.IsActive;


            var result = await _userManager.UpdateAsync(_user);
            if (result.Succeeded)
            {

                return ServiceResult.Success(true);

            }
            _logger.LogError(string.Join(",", result.Errors));
            return ServiceResult.Failed<bool>(ServiceError.ErrorInSaveOrUpdate);
        }
    }
}
