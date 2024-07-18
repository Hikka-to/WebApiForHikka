using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.WithSeoAddition.Animes;

public class AnimeService(IAnimeRepository repository)
    : CrudService<Anime, IAnimeRepository>(repository), IAnimeService
{
    public string? GetPosterPath(Guid animeId)
    {
        return RelationRepository.GetPosterPath(animeId);
    }

    public Task<string?> GetPosterPathAsync(Guid animeId)
    {
        return RelationRepository.GetPosterPathAsync(animeId);
    }
}