using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApplication.Application.Common.Interfaces
{
    public interface ISmsService<TResult>
    {
        void SendSms(IEnumerable<string> PhoneNumbers, object data);

        void SendSms(string phoneNumber, string message = "");

        void SendBulkSms(string[] phoneNumbers, string message = "");

        void SendSms(List<Guid> Ids);

        Task<TResult> SendVerifySms(string phoneNumber, string token = "", string title = "", string template = "verify");

        void SendForgetPasswordSms(string phoneNumber, string message = "");
    }
}
