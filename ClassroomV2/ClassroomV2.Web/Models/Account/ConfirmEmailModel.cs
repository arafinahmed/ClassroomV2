using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassroomV2.Web.Models.Account
{
    public class ConfirmEmailModel
    {
        public string StatusMessage { get; internal set; }
        public bool IsSuccess { get; internal set; }
    }
}
