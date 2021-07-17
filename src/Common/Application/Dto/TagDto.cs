using CleanApplication.Application.Common.Mappings;
using CleanApplication.Domain.Entities;
using System;

namespace CleanApplication.Application.Dto
{
    public class TagDto : IMapFrom<Tag>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string KeyName { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
