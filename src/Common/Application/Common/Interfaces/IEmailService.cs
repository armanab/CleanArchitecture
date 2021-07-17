using CleanApplication.Application.Common.Models;
using System.Threading;
using System.Threading.Tasks;

namespace CleanApplication.Application.Common.Interfaces
{
    public interface IEmailService
    {
        bool SendEmail(EmailApp email);

    }
}
