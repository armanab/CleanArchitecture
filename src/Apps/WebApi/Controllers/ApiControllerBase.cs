using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace CleanApplication.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public  class ApiControllerBase : ControllerBase
    {
        private ISender _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();

        protected List<string> CheckModelState()
        {

            var errors = ModelState.SelectMany(x => x.Value.Errors).Select(x=>x.ErrorMessage)
               .Where(y => y.Count() > 0)
               .ToList();
            return errors;

        }
        protected HttpResponseMessage RedirectToUrl(string redirectUrl, string p1 = null)
        {
            var url = redirectUrl;
            var parameters = new[] { p1 };

            url = parameters.Where(param => !string.IsNullOrEmpty(param)).Aggregate(url, (current, param) => current + (";" + param.Replace("/", "")));

            var pageHtml = @"<html><head><meta charset = 'utf-8'><title>درحال انتقال ...</title></head><body><script>window.location.replace('" + url + "')</script></body></html>";
            return new HttpResponseMessage
            {
                Content = new StringContent(pageHtml, Encoding.UTF8, "text/html") { }
            };
        }
    }
}
