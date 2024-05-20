﻿using WebApiForHikka.Application.RestrictedRatings;
using WebApiForHikka.Constants.Models.RestrictedRatings;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories;

public class RestrictedRatingRepository(HikkaDbContext dbContext) : CrudRepository<RestrictedRating>(dbContext), IRestrictedRatingRepository
{
    protected override IQueryable<RestrictedRating> Filter(IQueryable<RestrictedRating> query, string filterBy, string filter) => filterBy switch
    {
        RestrictedRatingStringConstants.NameName => query.Where(m => m.Name.Contains(filter, StringComparison.OrdinalIgnoreCase)),
        RestrictedRatingStringConstants.ValueName => query.Where(m => m.Value.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase)),
        RestrictedRatingStringConstants.HintName => query.Where(m => m.Hint.Contains(filter, StringComparison.OrdinalIgnoreCase)),
        RestrictedRatingStringConstants.IconName => query.Where(m => m.Icon != null && m.Icon.Contains(filter, StringComparison.OrdinalIgnoreCase)),
        _ => query.Where(m => m.Id.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase)),
    };

    protected override IQueryable<RestrictedRating> Sort(IQueryable<RestrictedRating> query, string orderBy, bool isAscending) => orderBy switch
    {
        RestrictedRatingStringConstants.NameName => isAscending ? query.OrderBy(m => m.Name) : query.OrderByDescending(m => m.Name),
        RestrictedRatingStringConstants.ValueName => isAscending ? query.OrderBy(m => m.Value) : query.OrderByDescending(m => m.Value),
        RestrictedRatingStringConstants.HintName => isAscending ? query.OrderBy(m => m.Hint) : query.OrderByDescending(m => m.Hint),
        RestrictedRatingStringConstants.IconName => isAscending ? query.OrderBy(m => m.Icon) : query.OrderByDescending(m => m.Icon),
        _ => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id) // Default sorting by Id
    };

    protected override void Update(RestrictedRating model, RestrictedRating entity)
    {
        entity.SeoAddition = model.SeoAddition;
        entity.Value = model.Value;
        entity.Hint = model.Hint;
        entity.Icon = model.Icon;
        entity.Name = model.Name;
    }
}