using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.Periods;
public class PeriodService : CrudService<Period, IPeriodRepository>, IPeriodService
{
    public PeriodService(IPeriodRepository repository) : base(repository)
    {
    }
}
