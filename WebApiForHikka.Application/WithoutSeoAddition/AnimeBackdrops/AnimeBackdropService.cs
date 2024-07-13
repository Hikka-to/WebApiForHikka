using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.AnimeBackdrops;

public class AnimeBackdropService(IAnimeBackdropRepository repository)
    : CrudService<AnimeBackdrop, IAnimeBackdropRepository>(repository),
        IAnimeBackdropService
{

    public Task<string?> GetImagePathAsync(Guid id)
    {
        return _repository.GetImagePathAsync(id);
    }
}