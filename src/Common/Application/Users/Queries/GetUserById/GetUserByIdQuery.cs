using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanApplication.Application.Common.Exceptions;
using CleanApplication.Application.Common.Interfaces;
using CleanApplication.Application.Common.Models;
using CleanApplication.Application.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanApplication.Application.Users.Queries.GetUserById
{

    public class GetUserByIdQuery : IRequestWrapper<ApplicationUserDto>
    {
        public Guid Id { get; set; }

    }
    public class GetUserByIdQueryHandler : IRequestHandlerWrapper<GetUserByIdQuery, ApplicationUserDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;

        public GetUserByIdQueryHandler(IApplicationDbContext context, IMapper mapper, IIdentityService Identity)
        {
            _context = context;
            _mapper = mapper;
            _identityService = Identity;
        }
        public async Task<ServiceResult<ApplicationUserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var _user = await _identityService.GetUserByIdAsync(request.Id, cancellationToken);
            var userDto = _mapper.Map<ApplicationUserDto>(_user);
            //.ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider)


            return userDto != null ? ServiceResult.Success(userDto) : throw new NotFoundException(nameof(ApplicationUserDto), request.Id);
        }
    }
}
