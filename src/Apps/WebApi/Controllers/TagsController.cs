using CleanApplication.Application.Common.Models;
using CleanApplication.Application.Tags.Queries.GetTags;
using CleanApplication.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanApplication.WebApi.Controllers
{
    [Authorize]
    public class TagsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ServiceResult<PaginatedList<TagDto>>>> Get([FromQuery] GetTagsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
