﻿using Microsoft.EntityFrameworkCore;
using System.Collections;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Extensions;

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
        var all = DbContext.Animes.ToList();
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

    public virtual async Task<PaginatedCollection<TModel>> GetAllAsync(FilterPagination dto, CancellationToken cancellationToken)
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

    public virtual async Task<IReadOnlyCollection<TModel?>> GetAllModelsByIdsAsync(List<Guid> ids, CancellationToken cancellationToken)
    {
        return await DbContext.Set<TModel>().Where(m => ids.Contains(m.Id)).ToArrayAsync(cancellationToken);
    }

    public virtual TModel? Get(Guid id)
    {
        return DbContext.Set<TModel>().FirstOrDefault(e => e.Id == id);
    }

    protected virtual void Update(TModel model, TModel entity)
    {
        DbContext.Entry(entity).CurrentValues.SetValues(model);
        var navigations = DbContext.Entry(entity).Navigations.ToList();

        foreach (var navigationEntry in DbContext.Entry(entity).Navigations)
        {
            navigationEntry.CurrentValue = DbContext.Entry(model).Navigation(navigationEntry.Metadata.Name).CurrentValue;
        }
    }

    protected virtual IQueryable<TModel> Filter(IQueryable<TModel> query, string filterBy, string filter)
    {
        var entityType = DbContext.Model.FindEntityType(typeof(TModel));
        Microsoft.EntityFrameworkCore.Metadata.INavigationBase[] navigations = entityType != null ? [
            ..entityType.GetNavigations(),
            ..entityType.GetSkipNavigations()
        ] : [];

        if (entityType?.FindProperty(filterBy) is { } property)
        {
            if (property.ClrType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(property.ClrType))
                query = query.FilterMany(filterBy, filter);
            else
                query = query.Filter(filterBy, filter);
        }
        else if (navigations.FirstOrDefault(n => n.Name == filterBy) is { } navigation)
        {
            var targetType = navigation.TargetEntityType;

            if (navigation.IsCollection)
            {
                if (targetType.FindProperty("Slug") != null)
                    query = query.FilterMany(filterBy, "Slug", filter);
                else if (targetType.FindProperty("Name") != null)
                    query = query.FilterMany(filterBy, "Name", filter);
                else if (targetType.FindPrimaryKey() is { } primaryKey)
                    query = query.FilterMany(filterBy, primaryKey.Properties[0].Name, filter);
            }
            else
            {
                if (targetType.FindProperty("Slug") != null)
                    query = query.Filter(filterBy + ".Slug", filter);
                else if (targetType.FindProperty("Name") != null)
                    query = query.Filter(filterBy + ".Name", filter);
                else if (targetType.FindPrimaryKey() is { } primaryKey)
                    query = query.Filter(filterBy + "." + primaryKey.Properties[0].Name, filter);
            }
        }
        else if (entityType?.FindPrimaryKey() is { } primaryKey)
        {
            query = query.Filter(primaryKey.Properties[0].Name, filter);
        }

        return query;
    }

    protected virtual IQueryable<TModel> Sort(IQueryable<TModel> query, string orderBy, bool isAscending) => query.Sort(orderBy, isAscending);
}