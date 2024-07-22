using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.AnimeBackdrops;

public interface IAnimeBackdropService : ICrudService<AnimeBackdrop>
{
    public Task<string?> GetImagePathAsync(Guid id);

    public IQueryable<AnimeBackdrop> GetAllBackdropForAnime(Guid id);
}