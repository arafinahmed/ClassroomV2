using Microsoft.AspNetCore.Identity;
using System;

namespace ClassroomV2.Membership.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Name { get; set; }
        
    }
}
