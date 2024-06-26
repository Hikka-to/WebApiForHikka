using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeBackdrops;
using WebApiForHikka.Constants.Models.AnimeBackdrops;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;

public class AnimeBackdropRepository(HikkaDbContext dbContext) : CrudRepository<AnimeBackdrop>(dbContext), IAnimeBackdropRepository
{
    protected override IQueryable<AnimeBackdrop> Filter(IQueryable<AnimeBackdrop> query, string filterBy, string filter) => filterBy switch
    {
        AnimeBackdropStringConstants.PathName => query.Where(m => EF.Functions.ILike(m.Path, $"%{filter}%")),
        AnimeBackdropStringConstants.AnimeName => query.Where(m => EF.Functions.ILike(m.Anime.Name, $"%{filter}%")),
        AnimeBackdropStringConstants.WidthName => query.Where(m => EF.Functions.ILike(m.Width.ToString(), $"%{filter}%")),
        AnimeBackdropStringConstants.HeightName => query.Where(m => EF.Functions.ILike(m.Height.ToString(), $"%{filter}%")),
        AnimeBackdropStringConstants.ColorsName => query.Where(m => m.Colors == null ?
            string.IsNullOrEmpty(filter) :
            m.Colors.Any(c => EF.Functions.ILike(c.ToString(), $"%{filter}%"))),
        _ => query.Where(m => m.Id.ToString().Contains(filter)),
    };

    protected override IQueryable<AnimeBackdrop> Sort(IQueryable<AnimeBackdrop> query, string orderBy, bool isAscending) => orderBy switch
    {
        AnimeBackdropStringConstants.PathName => isAscending ? query.OrderBy(m => m.Path) : query.OrderByDescending(m => m.Path),
        AnimeBackdropStringConstants.AnimeName => isAscending ? query.OrderBy(m => m.Anime.Name) : query.OrderByDescending(m => m.Anime.Name),
        AnimeBackdropStringConstants.WidthName => isAscending ? query.OrderBy(m => m.Width) : query.OrderByDescending(m => m.Width),
        AnimeBackdropStringConstants.HeightName => isAscending ? query.OrderBy(m => m.Height) : query.OrderByDescending(m => m.Height),
        AnimeBackdropStringConstants.ColorsName => isAscending ? query.OrderBy(m => m.Colors) : query.OrderByDescending(m => m.Colors),
        _ => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id),
    };

    protected override void Update(AnimeBackdrop model, AnimeBackdrop entity)
    {
        DbContext.Entry(entity).CurrentValues.SetValues(model);
        entity.Anime = model.Anime;
    }
}
