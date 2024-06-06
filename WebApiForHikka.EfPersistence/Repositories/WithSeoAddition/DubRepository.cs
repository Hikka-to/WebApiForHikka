using WebApiForHikka.Application.WithSeoAddition.Dubs;
using WebApiForHikka.Constants.Models.Dubs;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;

public class DubRepository : CrudRepository<Dub>, IDubRepository
{
    public DubRepository(HikkaDbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<Dub> Filter(IQueryable<Dub> query, string filterBy, string filter)
    {
        return filterBy switch
        {
            DubStringConstants.NameName => query.Where(m => m.Name.Contains(filter, StringComparison.OrdinalIgnoreCase)),
            DubStringConstants.IconName => query.Where(m => (m.Icon ?? "").Contains(filter, StringComparison.OrdinalIgnoreCase)),
            _ => query.Where(m => m.Id.ToString().Contains(filter)),
        };
    }

    protected override IQueryable<Dub> Sort(IQueryable<Dub> query, string orderBy, bool isAscending)
    {
        return orderBy switch
        {
            DubStringConstants.NameName => isAscending ? query.OrderBy(m => m.Name) : query.OrderByDescending(m => m.Name),
            DubStringConstants.IconName => isAscending ? query.OrderBy(m => m.Icon ?? "") : query.OrderByDescending(m => m.Icon ?? ""),
            _ => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id) // Default sorting by Id
        };
    }

    protected override void Update(Dub model, Dub entity)
    {
        DbContext.Entry(entity).CurrentValues.SetValues(model);
        entity.SeoAddition = model.SeoAddition;
    }
}
