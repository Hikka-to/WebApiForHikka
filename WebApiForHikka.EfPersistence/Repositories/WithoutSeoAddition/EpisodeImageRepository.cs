using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.WithoutSeoAddition.EpisodeImages;
using WebApiForHikka.Application.WithSeoAddition.Episodes;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;

public class EpisodeImageRepository(HikkaDbContext dbContext)
    : CrudRepository<EpisodeImage>(dbContext), IEpisodeImageRepository
{
    public async Task<string> GetImagePath(Guid id)
    {
        return (await DbContext.Set<EpisodeImage>().FirstOrDefaultAsync(i => i.Id == id))?.Path;
    }
}
