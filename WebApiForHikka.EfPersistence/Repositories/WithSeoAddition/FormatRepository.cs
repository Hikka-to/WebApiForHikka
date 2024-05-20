using WebApiForHikka.Application.Formats;
using WebApiForHikka.Constants.Models.Formats;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories;

public class FormatRepository(HikkaDbContext dbContext) : CrudRepository<Format>(dbContext), IFormatRepository
{
    protected override IQueryable<Format> Filter(IQueryable<Format> query, string filterBy, string filter) => filterBy switch
    {
        FormatStringConstants.NameName => query.Where(m => m.Name.Contains(filter, StringComparison.OrdinalIgnoreCase)),
        _ => query.Where(m => m.Id.ToString().Contains(filter)),
    };

    protected override IQueryable<Format> Sort(IQueryable<Format> query, string orderBy, bool isAscending) => orderBy switch
    {
        FormatStringConstants.NameName => isAscending ? query.OrderBy(m => m.Name) : query.OrderByDescending(m => m.Name),
        _ => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id) // Default sorting by Id
    };

    protected override void Update(Format model, Format entity)
    {
        entity.Name = model.Name;
        entity.SeoAddition = model.SeoAddition;
    }
}