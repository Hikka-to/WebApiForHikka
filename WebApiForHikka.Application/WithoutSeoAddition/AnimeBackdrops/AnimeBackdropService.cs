using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.WebApi.Helper.FileHelper;

namespace WebApiForHikka.Application.WithoutSeoAddition.AnimeBackdrops;

public class AnimeBackdropService(IAnimeBackdropRepository repository, IFileHelper fileHelper)
    : CrudService<AnimeBackdrop, IAnimeBackdropRepository>(repository),
        IAnimeBackdropService
{
    public IQueryable<AnimeBackdrop> GetAllBackdropForAnime(Guid id)
    {
        return _repository.GetAllBackdropForAnime(id);
    }

    public Task<string?> GetImagePathAsync(Guid id)
    {
        return _repository.GetImagePathAsync(id);
    }

    public override async Task DeleteAsync(Guid id, CancellationToken cancellationToken) 
    {
        var backdrop = await _repository.GetAsync(id, cancellationToken);

        fileHelper.DeleteFile(backdrop.Path);
        await _repository.DeleteAsync(id, cancellationToken);
    }
}