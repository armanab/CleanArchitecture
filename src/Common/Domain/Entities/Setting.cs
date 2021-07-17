using CleanApplication.Domain.Common;

namespace CleanApplication.Domain.Entities
{
    public class Setting:AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value  { get; set; }
        public string InputType { get; set; }
        public string Title { get; set; }
    }
}
