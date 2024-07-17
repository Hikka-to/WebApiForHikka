using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.WithSeoAddition.Animes;

public class AnimeService(IAnimeRepository repository)
    : CrudService<Anime, IAnimeRepository>(repository), IAnimeService
{
    public string? GetPosterPath(Guid animeId)
    {
        return _repository.GetPosterPath(animeId);
    }

    public Task<string?> GetPosterPathAsync(Guid animeId)
    {
        return _repository.GetPosterPathAsync(animeId);
    }
}
