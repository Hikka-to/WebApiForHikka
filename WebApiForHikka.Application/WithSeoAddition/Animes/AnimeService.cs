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
        return Repository.GetPosterPath(animeId);
    }

    public Task<string?> GetPosterPathAsync(Guid animeId)
    {
        return Repository.GetPosterPathAsync(animeId);
    }

    public override async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var backdrops =  animeBackdropService.GetAllBackdropForAnime(id);

        foreach (var item in backdrops) await animeBackdropService.DeleteAsync(item.Id, cancellationToken);

        var anime = await Repository.GetAsync(id, cancellationToken);

        if (anime?.PosterPath != null)
            fileHelper.DeleteFile(anime.PosterPath);

        await Repository.DeleteAsync(id, cancellationToken);
    }
}