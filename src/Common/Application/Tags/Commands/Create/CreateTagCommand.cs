using AutoMapper;
using CleanApplication.Application.Common.Interfaces;
using CleanApplication.Application.Common.Models;
using CleanApplication.Application.Dto;
using CleanApplication.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanApplication.Application.Tags.Commands.Create
{
    public class CreateTagCommand : IRequestWrapper<TagDto>
    {
        public string KeyName { get;  set; }
        public string Name { get;  set; }
    }
    public class CreateTagCommandHandler : IRequestHandlerWrapper<CreateTagCommand, TagDto>
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateTagCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResult<TagDto>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var entity = new Tag
            {
                KeyName = request.KeyName,
                Name=request.Name
                
            };

            await _context.Tags.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<TagDto>(entity));
        }
    }
}
