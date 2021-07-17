using CleanApplication.Application.Common.Enum;
using CleanApplication.Application.Common.Models;
using System.Threading;
using System.Threading.Tasks;

namespace CleanApplication.Application.Common.Interfaces
{
    public interface IHttpClientHandler
    {
        Task<ServiceResult<TResult>> GenericRequest<TRequest, TResult>(string clientApi, string url,
            CancellationToken cancellationToken,
            MethodType method = MethodType.Get,
            TRequest requestEntity = null)
            where TResult : class where TRequest : class;
    }
}
