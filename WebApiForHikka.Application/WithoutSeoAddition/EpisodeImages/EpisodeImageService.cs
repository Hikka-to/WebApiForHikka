using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.SharedFunction.Helpers.FileHelper;

namespace WebApiForHikka.Application.WithoutSeoAddition.EpisodeImages;

public class EpisodeImageService(IEpisodeImageRepository repository, IFileHelper fileHelper)
    : CrudService<EpisodeImage, IEpisodeImageRepository>(repository), IEpisodeImageService
{
    public Task<string?> GetImagePath(Guid id)
    {
        return Repository.GetImagePath(id);
    }

    public override async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var episodeImage = await Repository.GetAsync(id, cancellationToken);
        await Repository.DeleteAsync(id, cancellationToken);
        if (episodeImage?.Path != null)
            fileHelper.DeleteFile(episodeImage.Path);
    }

    public IQueryable<EpisodeImage> GetEpisodeImagesForEpisode(Guid id)
    {
        return Repository.GetEpisodeImagesForEpisode(id);
    }
}