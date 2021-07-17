using CleanApplication.Application.Common.Interfaces;
using CleanApplication.Application.Common.Models;
using CleanApplication.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanApplication.Application.Users.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequestWrapper<RefreshTokenResponseDto>
    {
        public string RefreshToken { get; set; }
        public string IpAddress { get; set; }

    }
    public class RefreshTokenCommandHandler : IRequestHandlerWrapper<RefreshTokenCommand, RefreshTokenResponseDto>
    {
        private readonly ICacheService _cacheService;
        IIdentityService _identityService;
        public RefreshTokenCommandHandler(IIdentityService identityService, ICacheService cacheService)
        {
            _identityService = identityService;
            _cacheService = cacheService;
        }
        public async Task<ServiceResult<RefreshTokenResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.RefreshToken(request.RefreshToken, request.IpAddress);

            if (result == null)
            {
                return ServiceResult.Failed<RefreshTokenResponseDto>(ServiceError.InvalidRefreshToken);
            }
            return ServiceResult.Success(result);
        }
    }
}
