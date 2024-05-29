using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Application.Statuses;
using WebApiForHikka.Constants.Models.Statuses;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories;
public class StatusRepository : CrudRepository<Status>, IStatusRepository
{
    public StatusRepository(HikkaDbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<Status> Filter(IQueryable<Status> query, string filterBy, string filter)
    {
        return filterBy switch
        {
            StatusStringConstants.NameName => query.Where(m => m.Name.Contains(filter, StringComparison.OrdinalIgnoreCase)),
            _ => query.Where(m => m.Id.ToString().Contains(filter)),
        };
    }

    protected override IQueryable<Status> Sort(IQueryable<Status> query, string orderBy, bool isAscending)
    {
        return orderBy switch
        {
            StatusStringConstants.NameName => isAscending ? query.OrderBy(m => m.Name) : query.OrderByDescending(m => m.Name),
            _ => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id)
        };
    }

    protected override void Update(Status model, Status entity)
    {
        DbContext.Entry(entity).CurrentValues.SetValues(model);
        entity.SeoAddition = model.SeoAddition;
    }
}
