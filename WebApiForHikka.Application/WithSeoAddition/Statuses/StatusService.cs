using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.WithSeoAddition.Statuses;

public class StatusService(IStatusRepository repository)
    : CrudService<Status, IStatusRepository>(repository),
        IStatusService;