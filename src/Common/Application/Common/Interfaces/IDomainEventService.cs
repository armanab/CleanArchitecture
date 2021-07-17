using CleanApplication.Domain.Common;
using System.Threading.Tasks;

namespace CleanApplication.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
