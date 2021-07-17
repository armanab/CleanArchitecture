using CleanApplication.Application.Common.Interfaces;
using CleanApplication.Application.Common.Models;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanApplication.Application.Users.Commands.ForgotPassword
{
    public class ForgotPasswordCommand : IRequestWrapper<bool>
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string SmsCode { get; set; }
    }
    public class ForgotPasswordCommandHandler : IRequestHandlerWrapper<ForgotPasswordCommand, bool>
    {
        IIdentityService _identityService;
        public ForgotPasswordCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;

        }
        public async Task<ServiceResult<bool>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
      
            return await _identityService.ChangePasswordAsync(request.Email, request.PhoneNumber, request.Password);
        }
    }
}
