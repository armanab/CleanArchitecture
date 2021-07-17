using CleanApplication.Application.Common.Mappings;
using CleanApplication.Domain.Entities;
using System;

namespace CleanApplication.Application.Dto
{
    public class SettingDto : IMapFrom<Setting>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Title { get; set; }
        public string InputType { get; set; }
        

    }
}
