using Microsoft.AspNetCore.Identity;
using System;

namespace ClassroomV2.Membership.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public Role():
            base()
        {
        }
        public Role(string roleName) :
            base(roleName)
        {

        }
    }
}
