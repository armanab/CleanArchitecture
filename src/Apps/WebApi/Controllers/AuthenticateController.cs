using CleanApplication.Application.Common.Models;
using CleanApplication.Application.Dto;
using CleanApplication.Application.Guests.Commands.GetGuestToken;

using CleanApplication.Application.Users.Commands.Create;
using CleanApplication.Application.Users.Commands.ForgotPassword;
using CleanApplication.Application.Users.Commands.RefreshToken;
using CleanApplication.Application.Users.Commands.RevokeToken;

using CleanApplication.Application.Users.Queries.GetToken;
using CleanApplication.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CleanApplication.WebApi.Controllers
{
    public class AuthenticateController : ApiControllerBase
    {
        #region const definition
        private string URL_EMAIL_CONFIRM = "/email/confirmEmail";

        #endregion


        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticateController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;



        }


        #region SingIn
        [HttpPost("signin")]
        public async Task<ActionResult<ServiceResult<LoginResponseDto>>> SignIn(GetTokenModel model)
        {
            var query = new GetTokenCommand
            {
                Email = model?.Email,
                Password = model?.Password,
                IpAddress = ipAddress()
            };
            return await Mediator.Send(query);

        }

        #endregion

        #region CreateUser
        [HttpPost("register")]
        public async Task<ActionResult<UserCreateDto>> Create(CreateUserCommand query)
        {
            return Ok(await Mediator.Send(query));
        }

      

        [HttpGet("confirmemail")]
        public async Task<HttpResponseMessage> ConfirmEmail(string token, string email)
        {
            string message = "";
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                BadRequest(new { errorCode = "UserNotFound", message = "User not found!" });

            }

            var userConfirmed = await _userManager.ConfirmEmailAsync(user, token);
            if (userConfirmed.Succeeded)
            {
                message = "EmailConfimSuccess";
            }
            else
            {
                message = string.Join(",", userConfirmed.Errors);

            }
            return RedirectToUrl(URL_EMAIL_CONFIRM, $"message={message},username={user.UserName}");
        }

        #endregion

        #region ForgotPassword


        [HttpPost("forgotpassword")]
        public async Task<ActionResult<ServiceResult<bool>>> ForgotPassword(ForgotPasswordCommand command)
        {
            return Ok(await Mediator.Send(command));

        }

        #endregion

        #region Guest

      

        [HttpPost("guestsignin")]
        public async Task<ActionResult<ServiceResult<GuestLoginResponseDto>>> GuestSignIn(GetGuestTokenCommand command)
        {
            return Ok(await Mediator.Send(command));

        }

        #endregion
        #region RefreshToken

        [HttpPost("refresh-token")]
        public async Task<ActionResult<ServiceResult<RefreshTokenResponseDto>>> RefreshToken([FromBody] string token)
        {

            var command = new RefreshTokenCommand
            {
                RefreshToken = token,
                IpAddress = ipAddress()
            };

            return Ok(await Mediator.Send(command));

        }


        [HttpPost("revoke-token")]
        public async Task<ActionResult<ServiceResult<RefreshTokenResponseDto>>> RevokeToken([FromBody] string token)
        {
            var command = new RevokeTokenCommand
            {
                Token = token,
                IpAddress = ipAddress()
            };

            return Ok(await Mediator.Send(command));
        }
        #endregion

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
        //private void setTokenCookie(string token)
        //{
        //    var cookieOptions = new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Expires = DateTime.UtcNow.AddDays(7)
        //    };
        //    Response.Cookies.Append("refreshToken", token, cookieOptions);
        //}
    }
}
