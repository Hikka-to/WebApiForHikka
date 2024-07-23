using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.EpisodeImages;

public class EpisodeImageService(IEpisodeImageRepository repository)
    : CrudService<EpisodeImage, IEpisodeImageRepository>(repository), IEpisodeImageService
{
    public Task<string> GetImagePath(Guid id)
    {
        return _repository.GetImagePath(id);
    }
}