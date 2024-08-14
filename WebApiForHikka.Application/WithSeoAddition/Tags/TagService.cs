using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.WithSeoAddition.Tags;

public class TagService(ITagRepository repository) : CrudService<Tag, ITagRepository>(repository), ITagService
{
    public Task<PaginatedCollection<Tag>> GetAllTagForCharactersAsync(FilterPagination dto, CancellationToken cancellationToken)
    {
        return repository.GetAllTagForCharactersAsync(dto, cancellationToken);
    }
}
