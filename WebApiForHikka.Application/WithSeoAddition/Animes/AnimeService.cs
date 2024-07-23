using WebApiForHikka.Application.Shared;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeBackdrops;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.SharedFunction.Helpers.FileHelper;

namespace WebApiForHikka.Application.WithSeoAddition.Animes;

public class AnimeService(
    IAnimeRepository repository,
    IAnimeBackdropService animeBackdropService,
    IFileHelper fileHelper)
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

    public override async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var backdrops = animeBackdropService.GetAllBackdropForAnime(id);

        foreach (var item in backdrops) await animeBackdropService.DeleteAsync(item.Id, cancellationToken);

        var anime = await _repository.GetAsync(id, cancellationToken);

        fileHelper.DeleteFile(anime.PosterPath);

        await _repository.DeleteAsync(id, cancellationToken);
    }
}