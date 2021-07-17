using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CleanApplication.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        [IgnoreDataMember] 
        public override string PasswordHash { get => base.PasswordHash; set => base.PasswordHash = value; }
        [IgnoreDataMember]
        public override string NormalizedEmail { get => base.NormalizedEmail; set => base.NormalizedEmail = value; }
        [IgnoreDataMember]
        public override string NormalizedUserName { get => base.NormalizedUserName; set => base.NormalizedUserName = value; }

        [IgnoreDataMember]
        public override string SecurityStamp { get => base.SecurityStamp; set => base.SecurityStamp = value; }
        [IgnoreDataMember]
        public override string ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; }
        [IgnoreDataMember]
        public override bool TwoFactorEnabled { get => base.TwoFactorEnabled; set => base.TwoFactorEnabled = value; }

        [IgnoreDataMember]
        public override int AccessFailedCount { get => base.AccessFailedCount; set => base.AccessFailedCount = value; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }


        /// <summary>
        /// 0 is famale and 1 is male
        /// </summary>
        public int Gender { get; set; }
        public DateTime Created { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
