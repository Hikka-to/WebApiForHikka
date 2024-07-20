using WebApiForHikka.Application.WithoutSeoAddition.EpisodeImages;
using WebApiForHikka.Application.WithSeoAddition.Episodes;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;

public class ImageEpisodeRepository(HikkaDbContext dbContext)
    : CrudRepository<EpisodeImage>(dbContext), IEpisodeImageRepository
{
    public Task<string> GetImagePath(Guid id)
    {
        throw new NotImplementedException();
    }
}
