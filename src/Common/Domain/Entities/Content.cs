using CleanApplication.Domain.Common;
using System;

namespace CleanApplication.Domain.Entities
{
  /// <summary>
  /// جدول محتوا
  /// </summary>
    public class Content : AuditableEntity
    {
        /// <summary>
        /// شناسه
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// محل قرارگیری محتوا در صفحه
        /// </summary>
        public string Slot { get; set; }
        /// <summary>
        /// اولویت
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// فعال 
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// عکس پروفایل
        /// </summary>
        public virtual Image Image { get; set; }

        public Guid? ImageId { get; set; }
        /// <summary>
        /// لینک نمایش
        /// </summary>
        public string Link { get; set; }
    }
}
