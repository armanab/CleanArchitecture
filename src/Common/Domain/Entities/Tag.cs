using CleanApplication.Domain.Common;
using System.Collections.Generic;

namespace CleanApplication.Domain.Entities
{
    public class Tag : AuditableEntity
    {
        /// <summary>
        /// شناسه
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// کلید
        /// </summary>
        public string KeyName { get; set; }
      
    }
}
