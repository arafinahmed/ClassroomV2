using System.Collections.Generic;

namespace ClassroomV2.Common.Utilities
{
    public interface IEmailService
    {
        void SendEmail(string receiver, string subject, string body);
        void BroadCast(IList<string> receivers, string subject, string body);
    }
}
