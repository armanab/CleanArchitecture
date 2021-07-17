using CleanApplication.Domain.Common;
using System;

namespace CleanApplication.Domain.Entities
{
    public class Image : AuditableEntity
    {
        /// <summary>
        /// شناسه
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// آدرس عکس
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// آدرس عکس کوچک
        /// </summary>
        public string ThumbnailUrl { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// پسوند فایل
        /// </summary>
        public string extension { get; set; }


    }
}
