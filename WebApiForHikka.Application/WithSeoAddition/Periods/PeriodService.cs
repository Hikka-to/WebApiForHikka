using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.WithSeoAddition.Periods;

public class PeriodService(IPeriodRepository repository)
    : CrudService<Period, IPeriodRepository>(repository), IPeriodService;