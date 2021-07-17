using CleanApplication.Domain.Common;
using CleanApplication.Domain.ValueObjects;
using System.Collections.Generic;

namespace CleanApplication.Domain.Entities
{
    public class TodoList : AuditableEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Colour Colour { get; set; } = Colour.White;

        public virtual ICollection<TodoItem> Items { get; private set; } = new List<TodoItem>();
    }
}
