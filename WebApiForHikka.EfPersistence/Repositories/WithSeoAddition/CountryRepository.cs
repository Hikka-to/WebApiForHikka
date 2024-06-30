using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.WithSeoAddition.Countries;
using WebApiForHikka.Constants.Models.Countries;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;

public class CountryRepository(HikkaDbContext dbContext) : CrudRepository<Country>(dbContext), ICountryRepository
{
    protected override IQueryable<Country> Filter(IQueryable<Country> query, string filterBy, string filter)
    {
        return filterBy switch
        {
            CountryStringConstants.NameName => query.Where(m => EF.Functions.ILike(m.Name, $"%{filter}%")),
            CountryStringConstants.IconName => query.Where(m => EF.Functions.ILike(m.Icon, $"%{filter}%")),
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
        DbContext.Entry(entity).CurrentValues.SetValues(model);
        entity.SeoAddition = model.SeoAddition;
    }
}
