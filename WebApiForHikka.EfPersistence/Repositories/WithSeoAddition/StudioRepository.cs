using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.WithSeoAddition.Studios;
using WebApiForHikka.Constants.Models.Studios;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;

public class StudioRepository : CrudRepository<Studio>, IStudioRepository
{
    public StudioRepository(HikkaDbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<Studio> Filter(IQueryable<Studio> query, string filterBy, string filter)
    {
        return filterBy switch
        {
            StudioStringConstants.NameName => query.Where(m => EF.Functions.ILike(m.Name, $"%{filter}%")),
            StudioStringConstants.LogoName => query.Where(m => EF.Functions.ILike(m.Logo ?? "", $"%{filter}%")),
            _ => query.Where(m => m.Id.ToString().Contains(filter)),
        };
    }

    protected override IQueryable<Studio> Sort(IQueryable<Studio> query, string orderBy, bool isAscending)
    {
        return orderBy switch
        {
            StudioStringConstants.NameName => isAscending ? query.OrderBy(m => m.Name) : query.OrderByDescending(m => m.Name),
            StudioStringConstants.LogoName => isAscending ? query.OrderBy(m => m.Logo ?? "") : query.OrderByDescending(m => m.Logo ?? ""),
            _ => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id)
        };
    }
}
