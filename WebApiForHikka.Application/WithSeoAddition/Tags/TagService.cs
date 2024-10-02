using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.WithSeoAddition.Tags;

public class TagService(ITagRepository repository)
    : CrudService<Tag, ITagRepository>(repository), ITagService
{
    private readonly ITagRepository _repository = repository;

    public Task<PaginatedCollection<Tag>> GetAllTagForCharactersAsync(FilterPagination dto,
        CancellationToken cancellationToken)
    {
        return _repository.GetAllTagForCharactersAsync(dto, cancellationToken);
    }
}