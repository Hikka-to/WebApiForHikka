using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.SharedFunction.Helpers.FileHelper;

namespace WebApiForHikka.Application.WithSeoAddition.Characters;

public class CharacterService(ICharacterRepository repository, IFileHelper fileHelper)
    : CrudService<Character, ICharacterRepository>(repository), ICharacterService
{
    public override async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var model = await GetAsync(id, cancellationToken);
        if (model != null)
        {
            fileHelper.DeleteFile(model.ImagePath);
        }
        await Repository.DeleteAsync(id, cancellationToken);
    }
}