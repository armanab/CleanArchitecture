using AutoMapper;
using CleanApplication.Application.Common.Interfaces;
using CleanApplication.Application.Common.Models;
using CleanApplication.Application.Dto;
using CleanApplication.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanApplication.Application.Contents.Commands.Create
{
    public class CreateContentCommand:IRequestWrapper<ContentDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Slot { get; set; }
        public int Priority { get; set; }
        public bool IsActive { get; set; }
        public Guid? ImageId { get; set; }
        public string Link { get; set; }
    }
    public class CreateContentCommandHandler : IRequestHandlerWrapper<CreateContentCommand, ContentDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateContentCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResult<ContentDto>> Handle(CreateContentCommand request, CancellationToken cancellationToken)
        {
            var entity = new Content
            {
                Name = request.Name,
                Description = request.Description,
                ImageId = request.ImageId,
                IsActive = request.IsActive,
                Priority = request.Priority,
                Slot = request.Slot,
                Link = request.Link
                
            };

            await _context.Contents.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<ContentDto>(entity));
        }
    }
}
