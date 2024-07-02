using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Application.WithSeoAddition.Tags;
using WebApiForHikka.Constants.Models.Tags;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;

public class TagRepository : CrudRepository<Tag>, ITagRepository
{
    public TagRepository(HikkaDbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<Tag> Filter(IQueryable<Tag> query, string filterBy, string filter)
    {


        return filterBy switch
        {
            TagStringConstants.NameName => query.Where(m => EF.Functions.ILike(m.Name, $"%{filter}%")),
            TagStringConstants.AlisesName => query.Where(m => m.Alises.Contains(filter)),
            TagStringConstants.IsGenreName => query.Where(m => EF.Functions.ILike(m.IsGenre.ToString(), $"%{filter}%")),
            TagStringConstants.EngNameName => query.Where(m => EF.Functions.ILike(m.EngName, $"%{filter}%")),
            TagStringConstants.ParentTagName => query.Where(m => m.ParentTag != null && EF.Functions.ILike(m.ParentTag.Name, $"%{filter}%")),
            _ => query.Where(m => m.Id.ToString().Contains(filter)),
        };
    }

    protected override IQueryable<Tag> Sort(IQueryable<Tag> query, string orderBy, bool isAscending)
    {
        return orderBy switch
        {
            TagStringConstants.NameName => isAscending ? query.OrderBy(m => m.Name) : query.OrderByDescending(m => m.Name),
            TagStringConstants.EngNameName => isAscending ? query.OrderBy(m => m.EngName) : query.OrderByDescending(m => m.EngName),
            TagStringConstants.AlisesName => isAscending ? query.OrderBy(m => m.Alises) : query.OrderByDescending(m => m.Alises),
            TagStringConstants.IsGenreName => isAscending ? query.OrderBy(m => m.IsGenre) : query.OrderByDescending(m => m.IsGenre),
            TagStringConstants.ParentTagName => isAscending ? query.OrderBy(m => m.ParentTag) : query.OrderByDescending(m => m.ParentTag),
            _ => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id)
        };
    }

    public override async Task<IReadOnlyCollection<Tag>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Set<Tag>().IgnoreAutoIncludes().Include(e => e.ParentTag).ToArrayAsync(cancellationToken);
    }

    public override async Task<PaginatedCollection<Tag>> GetAllAsync(FilterPagination dto, CancellationToken cancellationToken)
    {
        var skip = (dto.PageNumber - 1) * dto.PageSize;
        var take = dto.PageSize;

        var query = DbContext.Set<Tag>().AsQueryable();

        if (!string.IsNullOrWhiteSpace(dto.SearchTerm))
            query = Filter(query, dto.SortColumn, dto.SearchTerm);
        var totalItems = await query.CountAsync(cancellationToken);

        var orderBy = string.IsNullOrWhiteSpace(dto.SortColumn) ? SharedStringConstants.IdName : dto.SortColumn;

        query = Sort(query, orderBy, dto.SortOrder == SortOrder.Asc);

        var models = await query.Skip(skip).Take(take).ToArrayAsync(cancellationToken);

        return new PaginatedCollection<Tag>(models, totalItems);
    }

    public override async Task UpdateAsync(Tag model, CancellationToken cancellationToken)
    {
        var entity = await DbContext.Set<Tag>().FirstOrDefaultAsync(e => e.Id == model.Id, cancellationToken);
        if (entity is null)
            return;



        Update(model, entity);
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}

