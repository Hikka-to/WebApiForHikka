using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.WithSeoAddition.Animes;

public interface IAnimeRepository : ICrudRepository<Anime>
{
    public Task<string?> GetPosterPathAsync(Guid animeId);

    public string? GetPosterPath(Guid animeId);

}