using AutoMapper;
using CleanApplication.Application.Common.Interfaces;
using CleanApplication.Application.Common.Models;
using CleanApplication.Application.Dto;
using CleanApplication.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CleanApplication.Application.Images.Commands.Create
{
    public class CreateImageCommand : IRequestWrapper<ImageDto>
    {
        public IFormFile ImageFile { get; set; }
    }
    public class CreateImageCommandHandler : IRequestHandlerWrapper<CreateImageCommand, ImageDto>
    {

        public IImageService _imageService { get; set; }
        public IMapper _mapper { get; set; }
        public IApplicationDbContext _context { get; set; }
        public CreateImageCommandHandler(IImageService imageService, IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _imageService = imageService;
            _mapper = mapper;
        }
        public async Task<ServiceResult<ImageDto>> Handle(CreateImageCommand request, CancellationToken cancellationToken)
        {

            if (request.ImageFile.ContentType.ToLower() != "image/jpeg" &&
           request.ImageFile.ContentType.ToLower() != "image/jpg" &&
           request.ImageFile.ContentType.ToLower() != "image/png")
            {
                // not a .jpg or .png file
                return ServiceResult.Failed<ImageDto>(ServiceError.FileNotSupported);
            }


            string extension = Path.GetExtension(request.ImageFile.FileName);

            var _newId = Guid.NewGuid();
            string name = $"{_newId}_{DateTime.Now.ToString("yymmssfff")}";
          string thumbName = name + "TH";
            var url = await _imageService.FileUpload("", name, request.ImageFile, extension);
            var thumb = _imageService.ThumbImageUpload("", thumbName, request.ImageFile, extension, 120, 120);
            var entity = new Image
            {
                Url = url,
                ThumbnailUrl = thumb,
                Name = name,
                Id = _newId,
                extension = extension
            };

            await _context.Images.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);


            //string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
      


            return ServiceResult.Success(_mapper.Map<ImageDto>(entity));
        }
    }
}
