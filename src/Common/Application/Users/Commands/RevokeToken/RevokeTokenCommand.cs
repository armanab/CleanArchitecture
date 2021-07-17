using CleanApplication.Application.Common.Interfaces;
using CleanApplication.Application.Common.Models;
using CleanApplication.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanApplication.Application.Users.Commands.RevokeToken
{
    public class RevokeTokenCommand : IRequestWrapper<RevokeTokenResponseDto>
    {
        public string Token { get; set; }
        public string IpAddress { get; set; }
    }
    public class RevokeTokenCommandHandler : IRequestHandlerWrapper<RevokeTokenCommand, RevokeTokenResponseDto>
    {
        private readonly ICacheService _cacheService;
        IIdentityService _identityService;
        public RevokeTokenCommandHandler(IIdentityService identityService, ICacheService cacheService)
        {
            _identityService = identityService;
            _cacheService = cacheService;
        }
        public async Task<ServiceResult<RevokeTokenResponseDto>> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.RevokeToken(request.Token, request.IpAddress);

            if (result == null)
            {
                return ServiceResult.Failed<RevokeTokenResponseDto>(ServiceError.InvalidRefreshToken);
            }
            return ServiceResult.Success(result);
        }
    }
}
