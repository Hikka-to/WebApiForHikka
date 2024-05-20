using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.Periods;
public class PeriodService(IPeriodRepository repository) : CrudService<Period, IPeriodRepository>(repository), IPeriodService;