using System;

namespace CleanApplication.Domain.Common
{
    public abstract class AuditableEntity
    {
        public DateTime Created { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public Guid? LastModifiedBy { get; set; }
    }
}
