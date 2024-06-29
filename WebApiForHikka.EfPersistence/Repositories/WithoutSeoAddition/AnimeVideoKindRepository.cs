using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeVideoKinds;
using WebApiForHikka.Constants.Models.AnimeVideoKinds;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;

public class AnimeVideoKindRepository(HikkaDbContext dbContext) : CrudRepository<AnimeVideoKind>(dbContext), IAnimeVideoKindRepository
{
    protected override IQueryable<AnimeVideoKind> Filter(IQueryable<AnimeVideoKind> query, string filterBy, string filter) => filterBy switch
    {
        AnimeVideoKindStringConstants.NameName => query.Where(m => EF.Functions.ILike(m.Name, $"%{filter}%")),
        _ => query.Where(m => m.Id.ToString().Contains(filter)),
    };

    protected override IQueryable<AnimeVideoKind> Sort(IQueryable<AnimeVideoKind> query, string orderBy, bool isAscending) => orderBy switch
    {
        AnimeVideoKindStringConstants.NameName => isAscending ? query.OrderBy(m => m.Name) : query.OrderByDescending(m => m.Name),
        _ => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id),
    };

    protected override void Update(AnimeVideoKind model, AnimeVideoKind entity)
    {
        DbContext.Entry(entity).CurrentValues.SetValues(model);
    }
}
