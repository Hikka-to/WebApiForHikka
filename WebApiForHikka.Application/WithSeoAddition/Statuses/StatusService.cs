using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.Statuses;
public class StatusService : CrudService<Status, IStatusRepository>, IStatusService
{
    public StatusService(IStatusRepository repository) : base(repository)
    {
    }
}
