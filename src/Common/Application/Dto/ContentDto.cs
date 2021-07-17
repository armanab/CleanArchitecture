using CleanApplication.Application.Common.Mappings;
using CleanApplication.Domain.Entities;
using System;

namespace CleanApplication.Application.Dto
{
    public class ContentDto : IMapFrom<Content>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Slot { get; set; }
        public int Priority { get; set; }
        public bool IsActive { get; set; }
        public ImageDto Image { get; set; }
        public string Link { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }

    }
}
