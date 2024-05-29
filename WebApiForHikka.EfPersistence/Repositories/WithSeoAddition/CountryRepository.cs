using WebApiForHikka.Application.WithSeoAddition.Countries;
using WebApiForHikka.Constants.Models.Countries;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;

public class CountryRepository : CrudRepository<Country>, ICountryRepository
{
    public CountryRepository(HikkaDbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<Country> Filter(IQueryable<Country> query, string filterBy, string filter)
    {
        return filterBy switch
        {
            CountryStringConstants.NameName => query.Where(m => m.Name.Contains(filter, StringComparison.OrdinalIgnoreCase)),
            CountryStringConstants.IconName => query.Where(m => m.Icon.Contains(filter, StringComparison.OrdinalIgnoreCase)),
            _ => query.Where(m => m.Id.ToString().Contains(filter)),
        };
    }

    protected override IQueryable<Country> Sort(IQueryable<Country> query, string orderBy, bool isAscending)
    {
        return orderBy switch
        {
            CountryStringConstants.NameName => isAscending ? query.OrderBy(m => m.Name) : query.OrderByDescending(m => m.Name),
            CountryStringConstants.IconName => isAscending ? query.OrderBy(m => m.Icon) : query.OrderByDescending(m => m.Icon),
            _ => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id) // Default sorting by Id
        };
    }

    protected override void Update(Country model, Country entity)
    {
        entity.Icon = model.Icon;
        entity.Name = model.Name;
        entity.SeoAddition = model.SeoAddition;
    }
}
