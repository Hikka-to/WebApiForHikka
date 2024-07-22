using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using WebApiForHikka.Application.WithoutSeoAddition.EpisodeImages;
using WebApiForHikka.Application.WithSeoAddition.Episodes;
using WebApiForHikka.Constants.Models.WithoutSeoAddition.RelatedType;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;

public class EpisodeImageRepository(HikkaDbContext dbContext)
    : CrudRepository<EpisodeImage>(dbContext), IEpisodeImageRepository
{
    public IQueryable<EpisodeImage> GetEpisodeImagesForEpisode(Guid id)
    {
       return DbContext.Set<EpisodeImage>().Where(i => i.Episode.Id == id);
    }

    public async Task<string> GetImagePath(Guid id)
    {
        return (await DbContext.Set<EpisodeImage>().FirstOrDefaultAsync(i => i.Id == id))?.Path;
    }
}
