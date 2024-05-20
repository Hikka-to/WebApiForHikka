using WebApiForHikka.Application.Periods;
using WebApiForHikka.Constants.Periods;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories;
public class PeriodRepository(HikkaDbContext dbContext) : CrudRepository<Period>(dbContext), IPeriodRepository
{
    protected override IQueryable<Period> Filter(IQueryable<Period> query, string filterBy, string filter) => filterBy switch
    {
        PeriodStringConstants.NameName => query.Where(m => m.Name.Contains(filter, StringComparison.OrdinalIgnoreCase)),
        _ => query.Where(m => m.Id.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase)),
    };

    protected override IQueryable<Period> Sort(IQueryable<Period> query, string orderBy, bool isAscending) => orderBy switch
    {
        PeriodStringConstants.NameName => isAscending ? query.OrderBy(m => m.Name) : query.OrderByDescending(m => m.Name),
        _ => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id) // Default sorting by Id
    };

    protected override void Update(Period model, Period entity)
    {
        entity.Name = model.Name;
        entity.SeoAddition = model.SeoAddition;
    }
}