using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Domain;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Extensions;
using IModel = WebApiForHikka.Domain.Models.IModel;

namespace WebApiForHikka.EfPersistence.Repositories;

public abstract class CrudRepository<TModel> : ICrudRepository<TModel> where TModel : class, IModel
{
    protected readonly HikkaDbContext DbContext;

    protected CrudRepository(HikkaDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public virtual async Task<Guid> AddAsync(TModel model, CancellationToken cancellationToken)
    {
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

        if (!string.IsNullOrWhiteSpace(dto.SearchTerm))
            query = Filter(query, dto.Column, dto.SearchTerm);
        var totalItems = await query.CountAsync(cancellationToken);

        var orderBy = string.IsNullOrWhiteSpace(dto.Column) ? SharedStringConstants.IdName : dto.Column;

        query = Sort(query, orderBy, dto.SortOrder == SortOrder.Asc);

        var models = await query.Skip(skip).Take(take).ToArrayAsync(cancellationToken);

        return new PaginatedCollection<TModel>(models, totalItems);
    }

    public virtual async Task<IReadOnlyCollection<TModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Set<TModel>().ToArrayAsync(cancellationToken);
    }

    public virtual async Task<IReadOnlyCollection<TModel?>> GetAllModelsByIdsAsync(List<Guid> ids,
        CancellationToken cancellationToken)
    {
        return await DbContext.Set<TModel>().Where(m => ids.Contains(m.Id)).ToArrayAsync(cancellationToken);
    }

    public virtual TModel? Get(Guid id)
    {
        return DbContext.Set<TModel>().FirstOrDefault(e => e.Id == id);
    }

    protected virtual void Update(TModel model, TModel entity)
    {
        var modelEntry = DbContext.Entry(model);
        var entityEntry = DbContext.Entry(entity);
        foreach (var property in DbContext.Entry(entity).Properties)
            if (property.Metadata.IsPrimaryKey()) continue;
            else if (property.Metadata.Name == "CreatedAt") continue;
            else if (property.Metadata.Name == "UpdatedAt") property.CurrentValue = DateTime.UtcNow;
            else property.CurrentValue = modelEntry.Property(property.Metadata.Name).CurrentValue;

        foreach (var navigation in entityEntry.Navigations)
        {
            var modelNavigation = modelEntry.Navigation(navigation.Metadata.Name);
            var navigationMetadata = navigation.Metadata as INavigation;
            var foreignKey = navigationMetadata?.ForeignKey.Properties.First();
            var modelForeignKey = foreignKey != null ? modelEntry.CurrentValues[foreignKey.Name] : null;
            var entityForeignKey = foreignKey != null ? entityEntry.CurrentValues[foreignKey.Name] : null;
            if (modelNavigation.CurrentValue == null && foreignKey != null && !foreignKey.IsNullable) continue;
            if (modelNavigation.CurrentValue == null && modelForeignKey != null &&
                modelForeignKey != entityForeignKey) continue;

            navigation.CurrentValue = modelNavigation.CurrentValue;
        }
    }

    protected virtual IQueryable<TModel> Filter(IQueryable<TModel> query, string filterBy, string filter)
    {
        var entityType = DbContext.Model.FindEntityType(typeof(TModel));
        INavigationBase[] navigations = entityType != null
            ?
            [
                ..entityType.GetNavigations().Where(n =>
                    (n.FieldInfo?.IsPublic ?? false) || (n.PropertyInfo?.GetMethod?.IsPublic ?? false)),
                ..entityType.GetSkipNavigations().Where(n =>
                    (n.FieldInfo?.IsPublic ?? false) || (n.PropertyInfo?.GetMethod?.IsPublic ?? false))
            ]
            : [];

        if (entityType?.FindProperty(filterBy) is { } property &&
            ((property.FieldInfo?.IsPublic ?? false) || (property.PropertyInfo?.GetMethod?.IsPublic ?? false)))
        {
            if (property.ClrType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(property.ClrType))
                query = query.FilterMany(filterBy, filter);
            else
                query = query.Filter(filterBy, filter);
        }
        else if (navigations.FirstOrDefault(n => n.Name == filterBy) is { } navigation)
        {
            var targetType = navigation.TargetEntityType;
            string[] searchName =
            [
                "Slug",
                "Name"
            ];
            string? foundName = null;

            foreach (var name in searchName)
                if (targetType.FindProperty(name) is { } searchPropety &&
                    ((searchPropety.FieldInfo?.IsPublic ?? false) ||
                     (searchPropety.PropertyInfo?.GetMethod?.IsPublic ?? false)))
                {
                    foundName = name;
                    break;
                }

            if (foundName == null &&
                targetType.FindPrimaryKey() is { } primaryKey &&
                primaryKey.Properties.FirstOrDefault(p =>
                        (p.FieldInfo?.IsPublic ?? false) || (p.PropertyInfo?.GetMethod?.IsPublic ?? false)) is
                    { } primaryProperty)
                query = query.FilterMany(filterBy, primaryProperty.Name, filter);


            query = navigation.IsCollection
                ? query.FilterMany(filterBy, foundName!, filter)
                : query.Filter($"{filterBy}.{foundName}", filter);
        }
        else if (entityType?.FindPrimaryKey() is { } primaryKey &&
                 primaryKey.Properties.FirstOrDefault(p =>
                         (p.FieldInfo?.IsPublic ?? false) || (p.PropertyInfo?.GetMethod?.IsPublic ?? false)) is
                     { } primaryProperty)
        {
            query = query.Filter(primaryProperty.Name, filter);
        }

        return query;
    }

    protected virtual IQueryable<TModel> Sort(IQueryable<TModel> query, string orderBy, bool isAscending)
    {
        return query.Sort(orderBy, isAscending);
    }
}