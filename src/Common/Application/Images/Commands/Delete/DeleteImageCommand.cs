using AutoMapper;
using CleanApplication.Application.Common.Exceptions;
using CleanApplication.Application.Common.Interfaces;
using CleanApplication.Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanApplication.Application.Images.Commands.Delete
{
    public class DeleteImageCommand:IRequestWrapper<bool>
    {
        public Guid Id { get; set; }
    }
    public class DeleteImageCommandHandler : IRequestHandlerWrapper<DeleteImageCommand, bool>
    {
        public IImageService _imageService { get; set; }
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeleteImageCommandHandler(IApplicationDbContext context, IMapper mapper, IImageService imageService)
        {
            _context = context;
            _mapper = mapper;
            _imageService = imageService;

        }
        public async Task<ServiceResult<bool>> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Images
           .Where(l => l.Id == request.Id)
           .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Images), request.Id);
            }

            _imageService.RemoveFile(entity.Url,entity.Name,entity.extension);
            _imageService.RemoveFile(entity.ThumbnailUrl, $"{entity.Name}TH",entity.extension);

            _context.Images.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(true);
        }
    }
}
