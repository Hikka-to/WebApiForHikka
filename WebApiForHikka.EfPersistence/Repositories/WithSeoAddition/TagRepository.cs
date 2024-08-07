using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Application.WithSeoAddition.Tags;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;

public class TagRepository(HikkaDbContext dbContext) : CrudRepository<Tag>(dbContext), ITagRepository
{
    public override async Task<IReadOnlyCollection<Tag>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Set<Tag>().IgnoreAutoIncludes().Include(e => e.ParentTag)
            .ToArrayAsync(cancellationToken);
    }

    public virtual async Task<PaginatedCollection<Tag>> GetAllTagForCharactersAsync(FilterPagination dto,
        CancellationToken cancellationToken)
    {
        var skip = (dto.PageNumber - 1) * dto.PageSize;
        var take = dto.PageSize;

        var query = DbContext.Set<Tag>().Where(a => a.IsCharacterTag == true).AsQueryable();

        query = dto.Filters.Aggregate(query,
            (current, filter) => Filter(current, filter.Column, filter.SearchTerm, filter.IsStrict));
        var totalItems = await query.CountAsync(cancellationToken);

        var firstSort = dto.Sorts.FirstOrDefault();
        var otherSorts = dto.Sorts.Skip(1).ToArray();
        if (firstSort != null)
        {
            var orderedQuery = Sort(query, firstSort.Column, firstSort.SortOrder == SortOrder.Asc);
            query = otherSorts.Aggregate(orderedQuery,
                (current, sort) => ThenSort(current, sort.Column, sort.SortOrder == SortOrder.Asc));
        }

        var models = await query.Skip(skip).Take(take).ToArrayAsync(cancellationToken);

        return new PaginatedCollection<Tag>(models, totalItems);
    }
}