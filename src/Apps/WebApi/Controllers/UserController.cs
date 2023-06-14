using CleanApplication.Application.Common.Models;
using CleanApplication.Application.Dto;
using CleanApplication.Application.Users.Commands.Create;
using CleanApplication.Application.Users.Commands.Delete;
using CleanApplication.Application.Users.Commands.Update;
using CleanApplication.Application.Users.Queries.GetUserById;
using CleanApplication.Application.Users.Queries.GetUsersAdmin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CleanApplication.WebApi.Controllers
{
    [Authorize]
    public class UserController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ServiceResult<PaginatedList<ApplicationUserDto>>>> Get([FromQuery] GetUsersQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResult<ApplicationUserDto>>> GetById(Guid id)
        {
            return await Mediator.Send(new GetUserByIdQuery { Id = id });
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<ServiceResult<UserCreateDto>>> Create(CreateUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResult<ApplicationUserDto>>> Update(Guid id, UpdateUserCommand command)
        {
            if (id != command.Id)
            {
                return Ok(ServiceResult.Failed(ServiceError.InvalidId));
            }

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResult<bool>>> Delete(Guid id)
        {
            return await Mediator.Send(new DeleteUserCommand { Id = id });

        }
    }
}
