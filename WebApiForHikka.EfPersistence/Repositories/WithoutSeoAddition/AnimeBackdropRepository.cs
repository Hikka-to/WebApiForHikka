using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeBackdrops;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;

public class AnimeBackdropRepository(HikkaDbContext dbContext)
    : CrudRepository<AnimeBackdrop>(dbContext), IAnimeBackdropRepository
{
    public  IQueryable<AnimeBackdrop> GetAllBackdropForAnime(Guid id)
    {
        return  DbContext.Set<AnimeBackdrop>().Where(i => i.Anime.Id == id);
    }

    public async Task<string?> GetImagePathAsync(Guid id)
    {
        return (await DbContext.Set<AnimeBackdrop>().FirstOrDefaultAsync(i => i.Id == id))?.Path;
    }
}