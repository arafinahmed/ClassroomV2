using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassroomV2.Web.Models.Classroom
{
    public class SendMailModel
    {
        public string ReceiverMail { get; set; }
        public string SenderMail { get; set; }
        public int ClassId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

    }
}
