using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.WithSeoAddition.Tags;

public interface ITagRepository : ICrudRepository<Tag>
{
    public Task<PaginatedCollection<Tag>> GetAllTagForCharactersAsync(FilterPagination dto,
        CancellationToken cancellationToken);
}