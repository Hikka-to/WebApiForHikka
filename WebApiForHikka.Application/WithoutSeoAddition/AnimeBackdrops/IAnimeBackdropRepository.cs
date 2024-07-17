using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.AnimeBackdrops;

public interface IAnimeBackdropRepository : ICrudRepository<AnimeBackdrop> 
{
    public Task<string?> GetImagePathAsync(Guid id);
}