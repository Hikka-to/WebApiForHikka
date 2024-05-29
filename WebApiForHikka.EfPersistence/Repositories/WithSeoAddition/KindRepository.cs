using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Application.Kinds;
using WebApiForHikka.Constants.Models.Kinds;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories;
public class KindRepository : CrudRepository<Kind>, IKindRepository
{
    public KindRepository(HikkaDbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<Kind> Filter(IQueryable<Kind> query, string filterBy, string filter)
    {
         return filterBy switch
        {
            KindStringConstants.SlugName => query.Where(m => m.Slug.Contains(filter, StringComparison.OrdinalIgnoreCase)),
            KindStringConstants.HintName => query.Where(m => m.Hint.Contains(filter, StringComparison.OrdinalIgnoreCase)),
            _ => query.Where(m => m.Id.ToString().Contains(filter)),
        };
    }

    protected override IQueryable<Kind> Sort(IQueryable<Kind> query, string orderBy, bool isAscending)
    {
        return orderBy switch
        {
            KindStringConstants.SlugName => isAscending ? query.OrderBy(m => m.Slug) : query.OrderByDescending(m => m.Slug),
            KindStringConstants.HintName => isAscending ? query.OrderBy(m => m.Hint) : query.OrderByDescending(m => m.Hint),
            _ => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id) // Default sorting by Id

        };
    }

    protected override void Update(Kind model, Kind entity)
    {
        DbContext.Entry(entity).CurrentValues.SetValues(model);
        entity.SeoAddition = model.SeoAddition;
    }
}
