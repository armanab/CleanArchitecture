using AutoMapper;
using CleanApplication.Application.Common.Exceptions;
using CleanApplication.Application.Common.Interfaces;
using CleanApplication.Application.Common.Models;
using CleanApplication.Application.Dto;
using CleanApplication.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanApplication.Application.Tags.Commands.Update
{
    public class UpdateTagCommand : IRequestWrapper<TagDto>
    {
        public int Id { get;  set; }
        public string KeyName { get;  set; }
        public string Name { get;  set; }

    }
    public class UpdateTagCommandHandler : IRequestHandlerWrapper<UpdateTagCommand, TagDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateTagCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResult<TagDto>> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tags.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Tag), request.Id);
            }
            entity.KeyName = request.KeyName;
            entity.Name = request.Name;



            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<TagDto>(entity));
        }
    }
}
