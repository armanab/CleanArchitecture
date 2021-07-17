using Microsoft.AspNetCore.Identity;
using System;

namespace CleanApplication.Domain.Entities
{
    public class ApplicationRole: IdentityRole<Guid>
    {
        public ApplicationRole(string role): base(role)
        {
            
        }
        public ApplicationRole()
        {

        }
    }
}
