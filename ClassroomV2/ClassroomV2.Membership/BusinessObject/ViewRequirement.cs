using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomV2.Membership.BusinessObject
{
    public class ViewRequirement : IAuthorizationRequirement
    {
        public ViewRequirement() { }
    }
}
