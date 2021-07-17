using CleanApplication.Application.Common.Models;
using CleanApplication.Application.Settings.Queries.GetSettings;
using CleanApplication.Application.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using CleanApplication.Application.Settings.Queries.GetSettingById;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using System.Linq;

namespace CleanApplication.WebApi.Controllers
{
    [Authorize]
    public class SettingsController : ApiControllerBase
    {

        public SettingsController()
        {
        }
        [HttpGet]
        public async Task<ActionResult<ServiceResult<PaginatedList<SettingDto>>>> Get([FromQuery] GetSettingsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResult<SettingDto>>> GetById(int id)
        {
            return await Mediator.Send(new GetSettingByIdQuery { Id = id });
        }

     
    }
}
