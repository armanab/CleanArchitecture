using AutoMapper;
using CleanApplication.Application.Common.Exceptions;
using CleanApplication.Application.Common.Interfaces;
using CleanApplication.Application.Common.Models;
using CleanApplication.Application.Dto;
using CleanApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanApplication.Application.Users.Commands.Delete
{
    public class DeleteUserCommand:IRequestWrapper<bool>
    {
        public Guid Id { get; set; }

    }
    public class DeleteUserCommandHandler : IRequestHandlerWrapper<DeleteUserCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        public DeleteUserCommandHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
        {
            _context = context;
            _mapper = mapper;
            _identityService = identityService;
        }
        public async Task<ServiceResult<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {

           return await _identityService.DeleteUserAsync(request.Id);


            //return ServiceResult.Success(true);
        }
    }
}
