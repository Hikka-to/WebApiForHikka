using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.Statuses;

public class StatusService(IStatusRepository repository)
    : CrudService<Status, IStatusRepository>(repository),
        IStatusService;