using System.Threading;
using System.Threading.Tasks;
using CleanApplication.Application.Common.Interfaces;
using CleanApplication.Application.Common.Models;
using CleanApplication.Application.Dto;

namespace CleanApplication.Application.Users.Queries.GetToken
{

    public class GetTokenModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
    public class GetTokenCommand : IRequestWrapper<LoginResponseDto>
    {
        public string Email { get; set; }

        public string Password { get; set; }
        public string IpAddress { get; set; }

    }

    public class GetTokenQueryHandler : IRequestHandlerWrapper<GetTokenCommand, LoginResponseDto>
    {
        private readonly IIdentityService _identityService;
        //private readonly ITokenService _tokenService;

        public GetTokenQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
            //_tokenService = tokenService;
        }

        public async Task<ServiceResult<LoginResponseDto>> Handle(GetTokenCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.SignIn(request.Email, request.Password, request.IpAddress);

        }

    }
}
