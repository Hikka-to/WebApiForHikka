using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.SharedFunction.Helpers.FileHelper;

namespace WebApiForHikka.Application.WithoutSeoAddition.EpisodeImages;

public class EpisodeImageService(IEpisodeImageRepository repository, IFileHelper fileHelper)
    : CrudService<EpisodeImage, IEpisodeImageRepository>(repository), IEpisodeImageService
{
    public Task<string> GetImagePath(Guid id)
    {
        return _repository.GetImagePath(id);
    }

    public override async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var episodeImage = _repository.GetAsync(id, cancellationToken);
        await _repository.DeleteAsync(id, cancellationToken);
        fileHelper.DeleteFile((await episodeImage).Path);
    }

    public IQueryable<EpisodeImage> GetEpisodeImagesForEpisode(Guid id)
    {
        return _repository.GetEpisodeImagesForEpisode(id);
    }
}