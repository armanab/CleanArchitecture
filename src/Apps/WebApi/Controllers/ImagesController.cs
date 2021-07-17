using CleanApplication.Application.Common.Models;
using CleanApplication.Application.Dto;
using CleanApplication.Application.Images.Queries.GetImages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanApplication.WebApi.Controllers
{
    [Authorize]
    public class ImagesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ServiceResult<PaginatedList<ImageDto>>>> Get([FromQuery] GetImagesQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
