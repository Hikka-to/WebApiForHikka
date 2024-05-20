using WebApiForHikka.Application.Sources;
using WebApiForHikka.Constants.Models.Sources;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories;

public class SourceRepository(HikkaDbContext dbContext) : CrudRepository<Source>(dbContext), ISourceRepository
{
    protected override IQueryable<Source> Filter(IQueryable<Source> query, string filterBy, string filter) => filterBy switch
    {
        SourceStringConstants.NameName => query.Where(m => m.Name.Contains(filter, StringComparison.OrdinalIgnoreCase)),
        _ => query.Where(m => m.Id.ToString().Contains(filter)),
    };

    protected override IQueryable<Source> Sort(IQueryable<Source> query, string orderBy, bool isAscending) => orderBy switch
    {
        SourceStringConstants.NameName => isAscending ? query.OrderBy(m => m.Name) : query.OrderByDescending(m => m.Name),
        _ => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id)
    };

    protected override void Update(Source model, Source entity)
    {
        entity.SeoAddition = model.SeoAddition;
        entity.Name = model.Name;

    }
}