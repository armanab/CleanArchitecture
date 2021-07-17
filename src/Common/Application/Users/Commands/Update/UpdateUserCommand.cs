using AutoMapper;
using CleanApplication.Application.Common.Exceptions;
using CleanApplication.Application.Common.Interfaces;
using CleanApplication.Application.Common.Models;
using CleanApplication.Application.Dto;
using CleanApplication.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanApplication.Application.Users.Commands.Update
{
    public class UpdateUserCommand : IRequestWrapper<ApplicationUserDto>
    {
        public Guid Id { get; set; }
        //public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateOfBirth { get; set; }
        //public string Password { get; set; }

    }
    public class UpdateUserCommandHandler : IRequestHandlerWrapper<UpdateUserCommand, ApplicationUserDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;


        public UpdateUserCommandHandler(IApplicationDbContext context, IMapper mapper, IIdentityService Identity)
        {
            _context = context;
            _mapper = mapper;
            _identityService = Identity;

        }
        public async Task<ServiceResult<ApplicationUserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {

            //var entity = await _identityService.GetUserByIdAsync(request.Id, cancellationToken);

            //if (entity == null || entity.Email == "administrator@localhost.com")
            //{
            //    throw new NotFoundException(nameof(ApplicationUserDto), request.Id);
            //}
            var entity = new ApplicationUser
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                IsActive = request.IsActive,
                //PasswordHash = request.Password,

            };

            await _identityService.Update(entity);

            //await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<ApplicationUserDto>(entity));
        }
    }
}
