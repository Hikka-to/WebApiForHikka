using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.Sources;
using WebApiForHikka.Constants.Models.Sources;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories;

public class SourceRepository : CrudRepository<Source>, ISourceRepository
{
    public SourceRepository(HikkaDbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<Source> Filter(IQueryable<Source> query, string filterBy, string filter)
    {
        return filterBy switch
        {
            SourceStringConstants.NameName => query.Where(m => EF.Functions.ILike(m.Name, $"%{filter}%")),
            _ => query.Where(m => m.Id.ToString().Contains(filter)),
        };
    }

    protected override IQueryable<Source> Sort(IQueryable<Source> query, string orderBy, bool isAscending)
    {
        return orderBy switch
        {
            SourceStringConstants.NameName => isAscending ? query.OrderBy(m => m.Name) : query.OrderByDescending(m => m.Name),
            _ => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id)
        };

    }
}
