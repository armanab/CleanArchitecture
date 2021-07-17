using AutoMapper;
using CleanApplication.Application.Common.Exceptions;
using CleanApplication.Application.Common.Interfaces;
using CleanApplication.Application.Common.Models;
using CleanApplication.Application.Dto;
using CleanApplication.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanApplication.Application.Contents.Commands.Update
{
    public class UpdateContentCommand : IRequestWrapper<ContentDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Slot { get; set; }
        public int Priority { get; set; }
        public bool IsActive { get; set; }
        public Guid? ImageId { get; set; }
        public string Link { get; set; }
    }
    public class UpdateContentCommandHandler : IRequestHandlerWrapper<UpdateContentCommand, ContentDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateContentCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResult<ContentDto>> Handle(UpdateContentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Contents.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Content), request.Id);
            }
                entity.Name = request.Name;
                entity.Description = request.Description;
                entity.ImageId = request.ImageId;
                entity.IsActive = request.IsActive;
                entity.Priority = request.Priority;
                entity.Slot = request.Slot;
                entity.Link = request.Link;

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<ContentDto>(entity));
        }
    }
}
