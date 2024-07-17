using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;

public class AnimeRepository(HikkaDbContext dbContext) : CrudRepository<Anime>(dbContext), IAnimeRepository
{
    public string? GetPosterPath(Guid animeId)
    {
        return DbContext.Set<Anime>().FirstOrDefault(i => i.Id == animeId)?.PosterPath;
    }

    public async Task<string?> GetPosterPathAsync(Guid animeId)
    {
        return (await DbContext.Set<Anime>().FirstOrDefaultAsync(i => i.Id == animeId))?.PosterPath;
    }
}
