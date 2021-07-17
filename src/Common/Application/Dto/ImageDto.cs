using CleanApplication.Application.Common.Mappings;
using CleanApplication.Domain.Entities;
using System;

namespace CleanApplication.Application.Dto
{
    public class ImageDto:IMapFrom<Image>
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }

    }
}
