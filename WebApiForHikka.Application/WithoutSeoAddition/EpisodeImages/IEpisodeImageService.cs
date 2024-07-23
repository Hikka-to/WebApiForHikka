using System.Reflection.Emit;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.EpisodeImages;

public interface IEpisodeImageService : ICrudService<EpisodeImage> 
{

    public Task<string> GetImagePath(Guid id);

    public IQueryable<EpisodeImage> GetEpisodeImagesForEpisode(Guid id);

}
