using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.RestrictedRatings;
using WebApiForHikka.Constants.Models.RestrictedRatings;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories;

public class RestrictedRatingRepository : CrudRepository<RestrictedRating>, IRestrictedRatingRepository
{
    public RestrictedRatingRepository(HikkaDbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<RestrictedRating> Filter(IQueryable<RestrictedRating> query, string filterBy, string filter)
    {
        return filterBy switch
        {
            RestrictedRatingStringConstants.NameName => query.Where(m => EF.Functions.ILike(m.Name, $"%{filter}%")),
            RestrictedRatingStringConstants.ValueName => query.Where(m => EF.Functions.ILike(m.Value.ToString(), $"%{filter}%")),
            RestrictedRatingStringConstants.HintName => query.Where(m => EF.Functions.ILike(m.Hint, $"%{filter}%")),
            RestrictedRatingStringConstants.IconName => query.Where(m => m.Icon != null && EF.Functions.ILike(m.Icon, $"%{filter}%")),
            _ => query.Where(m => EF.Functions.ILike(m.Id.ToString(), $"%{filter}%")),
        };
    }

    protected override IQueryable<RestrictedRating> Sort(IQueryable<RestrictedRating> query, string orderBy, bool isAscending)
    {
        return orderBy switch
        {
            RestrictedRatingStringConstants.NameName => isAscending ? query.OrderBy(m => m.Name) : query.OrderByDescending(m => m.Name),
            RestrictedRatingStringConstants.ValueName => isAscending ? query.OrderBy(m => m.Value) : query.OrderByDescending(m => m.Value),
            RestrictedRatingStringConstants.HintName => isAscending ? query.OrderBy(m => m.Hint) : query.OrderByDescending(m => m.Hint),
            RestrictedRatingStringConstants.IconName => isAscending ? query.OrderBy(m => m.Icon) : query.OrderByDescending(m => m.Icon),
            _ => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id) // Default sorting by Id

        };
    }

    protected override void Update(RestrictedRating model, RestrictedRating entity)
    {

        DbContext.Entry(entity).CurrentValues.SetValues(model);
        entity.SeoAddition = model.SeoAddition;
    }
}
