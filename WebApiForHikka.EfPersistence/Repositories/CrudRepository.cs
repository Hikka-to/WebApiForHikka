using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;

namespace SushiRestaurant.EfPersistence.Repositories;

public abstract class CrudRepository<TModel> : ICrudRepository<TModel> where TModel : Model
{
    protected readonly HikkaDbContext DbContext;

    protected CrudRepository(HikkaDbContext dbContext)
    {
        DbContext = dbContext;
    }
    public async Task<Guid> AddAsync(TModel model, CancellationToken cancellationToken)
    {
        await DbContext.Set<TModel>().AddAsync(model, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
        return model.Id;
    }

    public async Task UpdateAsync(TModel model, CancellationToken cancellationToken)
    {
        var entity = await DbContext.Set<TModel>().FirstOrDefaultAsync(e => e.Id == model.Id);
        if (entity is null)
            return;

        Update(model, entity);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await DbContext.Set<TModel>().FirstOrDefaultAsync(e => e.Id == id);
        if (entity is null)
            return;

        DbContext.Set<TModel>().Remove(entity);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<TModel?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await DbContext.Set<TModel>().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<PaginatedCollection<TModel>> GetAllAsync(FilterPaginationDto dto, CancellationToken cancellationToken)
    {
        var skip = (dto.PageNumber - 1) * dto.PageSize;
        var take = dto.PageSize;

        var query = DbContext.Set<TModel>().AsQueryable();

        if (!string.IsNullOrWhiteSpace(dto.SearchTerm))
            query = Filter(query, dto.SortColumn, dto.SearchTerm);
        var totalItems = await query.CountAsync(cancellationToken);

        var orderBy = string.IsNullOrWhiteSpace(dto.SortColumn) ? SharedStringConstants.IdName : dto.SortColumn;

        query = Sort(query, orderBy, dto.SortOrder == SortOrder.Asc);

        var models = await query.Skip(skip).Take(take).ToArrayAsync(cancellationToken);

        return new PaginatedCollection<TModel>(models, totalItems);
    }

    public async Task<IReadOnlyCollection<TModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Set<TModel>().ToArrayAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<TModel?>> GetAllModelsByIdsAsync(List<Guid> ids, CancellationToken cancellationToken)
    {
        return await DbContext.Set<TModel>().Where(m => ids.Contains(m.Id)).ToArrayAsync(cancellationToken);

    }

    protected abstract void Update(TModel model, TModel entity);

    protected abstract IQueryable<TModel> Filter(IQueryable<TModel> query, string filterBy, string filter);

    protected abstract IQueryable<TModel> Sort(IQueryable<TModel> query, string orderBy, bool isAscending);


}