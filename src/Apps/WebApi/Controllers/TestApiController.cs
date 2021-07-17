using CleanApplication.Application.Common.Interfaces;
using CleanApplication.Application.Common.Models;
using CleanApplication.Application.Dto;
using CleanApplication.Application.Guests.Commands.GetGuestToken;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanApplication.WebApi.Controllers
{
    public class TestApiController : ApiControllerBase
    {
        private readonly IIdentityService _identityService;
        public TestApiController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        [HttpPost("testguestsignin")]
        public async Task<ActionResult<ServiceResult<GuestLoginResponseDto>>> GuestSignIn(GetGuestTokenCommand command)
        {
            await Task.Run(() =>
            {
                int result = 1 + 2;
            });
            var newId = Guid.NewGuid();
            return Ok(ServiceResult.Success(new GuestLoginResponseDto
            {
                Created = DateTime.Now,
                FirstName = command.FirstName,
                Id = newId,
                LastName = command.LastName,
                Token = _identityService.CreateJwtToken(newId.ToString(), DateTime.UtcNow.AddDays(7))
            }));
        }
    }
}
