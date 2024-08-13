using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.SharedFunction.Helpers.FileHelper;

namespace WebApiForHikka.Application.WithoutSeoAddition.AnimeBackdrops;

public class AnimeBackdropService(IAnimeBackdropRepository repository, IFileHelper fileHelper)
    : CrudService<AnimeBackdrop, IAnimeBackdropRepository>(repository),
        IAnimeBackdropService
{
    public IQueryable<AnimeBackdrop> GetAllBackdropForAnime(Guid id)
    {
        return Repository.GetAllBackdropForAnime(id);
    }

    public Task<string?> GetImagePathAsync(Guid id)
    {
        return Repository.GetImagePathAsync(id);
    }

    public override async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var backdrop = await Repository.GetAsync(id, cancellationToken);

        if (backdrop?.Path != null)
            fileHelper.DeleteFile(backdrop.Path);
        await Repository.DeleteAsync(id, cancellationToken);
    }
}