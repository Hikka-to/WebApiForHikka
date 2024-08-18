using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Extensions;
using WebApiForHikka.SharedFunction.Filtering;
using IModel = WebApiForHikka.Domain.Models.IModel;

namespace WebApiForHikka.EfPersistence.Repositories;

public abstract class CrudRepository<TModel>(HikkaDbContext dbContext) : ICrudRepository<TModel>
    where TModel : class, IModel
{
    protected readonly HikkaDbContext DbContext = dbContext;

    public virtual async Task<Guid> AddAsync(TModel model, CancellationToken cancellationToken)
    {
        var modelEntry = DbContext.Entry(model);
        if (modelEntry.Properties.FirstOrDefault(p => p.Metadata.Name == "CreatedAt") is { } createdAtProperty)
            createdAtProperty.CurrentValue = DateTime.UtcNow;
        if (modelEntry.Properties.FirstOrDefault(p => p.Metadata.Name == "UpdatedAt") is { } updatedAtProperty)
            updatedAtProperty.CurrentValue = DateTime.UtcNow;

        await DbContext.Set<TModel>().AddAsync(model, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
        return model.Id;
    }

    public virtual async Task UpdateAsync(TModel model, CancellationToken cancellationToken)
    {
        var entity = await DbContext.Set<TModel>().FirstOrDefaultAsync(e => e.Id == model.Id, cancellationToken);
        if (entity is null)
            return;

        Update(model, entity);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await DbContext.Set<TModel>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (entity is null)
            return;

        DbContext.Set<TModel>().Remove(entity);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<TModel?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await DbContext.Set<TModel>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public virtual async Task<PaginatedCollection<TModel>> GetAllAsync(FilterPagination dto,
        CancellationToken cancellationToken)
    {
        var skip = (dto.PageNumber - 1) * dto.PageSize;
        var take = dto.PageSize;

        var query = DbContext.Set<TModel>().AsQueryable();

        query = dto.Filters.Aggregate(query,
            (current, filter) => Filter(current, filter));
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

        return new PaginatedCollection<TModel>(models, totalItems);
    }

    public virtual async Task<ICollection<TModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Set<TModel>().ToArrayAsync(cancellationToken);
    }

    public virtual async Task<ICollection<TModel?>> GetAllModelsByIdsAsync(List<Guid> ids,
        CancellationToken cancellationToken)
    {
        return await DbContext.Set<TModel>().Where(m => ids.Contains(m.Id)).ToArrayAsync(cancellationToken);
    }

    public virtual TModel? Get(Guid id)
    {
        return DbContext.Set<TModel>().FirstOrDefault(e => e.Id == id);
    }

    protected virtual void Update<TEntityModel, TUpdateModel>(TUpdateModel model, TEntityModel entity)
        where TEntityModel : TModel
        where TUpdateModel : TEntityModel
    {
        var entityEntry = DbContext.Entry(entity);
        if (!entityEntry.Metadata.ClrType.IsInstanceOfType(model))
            throw new InvalidOperationException("Model and entity must be of the same type");

        var modelEntry = DbContext.Entry(model);
        foreach (var property in entityEntry.Properties)
            if (!property.Metadata.IsPrimaryKey() &&
                !property.Metadata.IsShadowProperty() &&
                ((property.Metadata.PropertyInfo?.SetMethod?.IsPublic ?? false) ||
                 (property.Metadata.FieldInfo?.IsPublic ?? false)))
                switch (property.Metadata.Name)
                {
                    case "CreatedAt":
                        continue;
                    case "UpdatedAt":
                        property.CurrentValue = DateTime.UtcNow;
                        break;
                    default:
                        property.CurrentValue = modelEntry.Property(property.Metadata.Name).CurrentValue;
                        break;
                }

        foreach (var navigation in entityEntry.Navigations)
        {
            var modelNavigation = modelEntry.Navigation(navigation.Metadata.Name);
            var navigationMetadata = navigation.Metadata as INavigation;
            var foreignKey = navigationMetadata?.ForeignKey.Properties[0];
            var modelForeignKey = foreignKey != null && foreignKey.DeclaringType.ClrType == entity.GetType()
                ? modelEntry.CurrentValues[foreignKey.Name]
                : null;
            var entityForeignKey = foreignKey != null && foreignKey.DeclaringType.ClrType == entity.GetType()
                ? entityEntry.CurrentValues[foreignKey.Name]
                : null;
            switch (modelNavigation.CurrentValue)
            {
                case null when foreignKey is { IsNullable: false }:
                case null when modelForeignKey != null &&
                               modelForeignKey != entityForeignKey:
                    continue;
                default:
                    navigation.CurrentValue = modelNavigation.CurrentValue;
                    break;
            }
        }
    }

    protected virtual IQueryable<TModel> Filter(IQueryable<TModel> query, IEnumerable<Filter> filters)
    {
        var entityType = DbContext.Model.FindEntityType(typeof(TModel)) ??
                         throw new InvalidOperationException($"Entity type for {typeof(TModel)} not found.");

        filters = filters.ToArray();
        var errors = filters
            .Where(filter => !FilterColumnSelector.IsColumnValidByReadablePath(entityType, filter.Column))
            .Select(filter => filter.Column)
            .ToArray();

        if (errors.Length != 0)
            throw new InvalidOperationException(
                $"Columns [{string.Join(", ", errors)}] are not valid for {typeof(TModel)} filter");

        var actualFilters = filters.Select(filter => filter with
        {
            Column = FilterColumnSelector.GetColumnByReadablePath(entityType, filter.Column).GetActualPath()
        });

        return query.Filter(actualFilters);
    }

    protected virtual IOrderedQueryable<TModel> Sort(IQueryable<TModel> query, string orderBy, bool isAscending)
    {
        var entityType = DbContext.Model.FindEntityType(typeof(TModel)) ??
                         throw new InvalidOperationException($"Entity type for {typeof(TModel)} not found.");

        if (SortColumnSelector.TryGetColumnByReadablePath(entityType, orderBy, out var column))
            return query.Sort(column.GetActualPath(), isAscending);

        throw new InvalidOperationException($"Column {orderBy} is not valid for {typeof(TModel)} sort");
    }

    protected virtual IOrderedQueryable<TModel> ThenSort(IOrderedQueryable<TModel> query, string orderBy,
        bool isAscending)
    {
        var entityType = DbContext.Model.FindEntityType(typeof(TModel)) ??
                         throw new InvalidOperationException($"Entity type for {typeof(TModel)} not found.");

        if (SortColumnSelector.TryGetColumnByReadablePath(entityType, orderBy, out var column))
            return query.ThenSort(column.GetActualPath(), isAscending);

        throw new InvalidOperationException($"Column {orderBy} is not valid for {typeof(TModel)} sort");
    }
}