using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Constants.Models.Animes;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;

public class AnimeRepository : CrudRepository<Anime>, IAnimeRepository
{
    public AnimeRepository(HikkaDbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<Anime> Filter(IQueryable<Anime> query, string filterBy, string filter)
    {
        return filterBy switch
        {
            AnimeStringConstants.IdName => query.Where(a => a.Id.ToString().Contains(filter)),
            AnimeStringConstants.TagsName => query.Where(a => a.TagsString.Contains(filter, StringComparison.OrdinalIgnoreCase)),
            AnimeStringConstants.CountriesName => query.Where(a => a.CountriesString.Contains(filter, StringComparison.OrdinalIgnoreCase)),
            AnimeStringConstants.DubsName => query.Where(a => a.DubsString.Contains(filter, StringComparison.OrdinalIgnoreCase)),
            AnimeStringConstants.KindName => query.Where(a => a.Kind.Slug.Contains(filter, StringComparison.OrdinalIgnoreCase)),
            AnimeStringConstants.StatusName => query.Where(a => a.Status.Name.Contains(filter, StringComparison.OrdinalIgnoreCase)),
            AnimeStringConstants.PeriodName => query.Where(a => a.Period.Name.Contains(filter, StringComparison.OrdinalIgnoreCase)),
            AnimeStringConstants.RestrictedRatingName => query.Where(a => a.RestrictedRating.Name.Contains(filter, StringComparison.OrdinalIgnoreCase)),
            AnimeStringConstants.SourceName => query.Where(a => a.Source.Name.Contains(filter, StringComparison.OrdinalIgnoreCase)),
            AnimeStringConstants.NameName => query.Where(a => a.Name.Contains(filter, StringComparison.OrdinalIgnoreCase)),
            AnimeStringConstants.ImageNameName => query.Where(a => a.ImageName != null && a.ImageName.Contains(filter, StringComparison.OrdinalIgnoreCase)),
            AnimeStringConstants.RomajiNameName => query.Where(a => a.RomajiName != null && a.RomajiName.Contains(filter, StringComparison.OrdinalIgnoreCase)),
            AnimeStringConstants.NativeNameName => query.Where(a => a.NativeName.Contains(filter, StringComparison.OrdinalIgnoreCase)),
            AnimeStringConstants.PosterPathName => query.Where(a => a.PosterPath.Contains(filter, StringComparison.OrdinalIgnoreCase)),
            AnimeStringConstants.PosterColorsName => query.Where(a => a.PosterColors.Any(pc => pc.ToString().Contains(filter))),
            AnimeStringConstants.AvgDurationName => query.Where(a => a.AvgDuration.ToString().Contains(filter)),
            AnimeStringConstants.HowManyEpisodesName => query.Where(a => a.HowManyEpisodes.ToString().Contains(filter)),
            AnimeStringConstants.FirstAirDateName => query.Where(a => a.FirstAirDate.ToString("yyyy-MM-dd").Contains(filter)),
            AnimeStringConstants.LastAirDateName => query.Where(a => a.LastAirDate.ToString("yyyy-MM-dd").Contains(filter)),
            AnimeStringConstants.TmdbIdName => query.Where(a => a.TmdbId.ToString().Contains(filter)),
            AnimeStringConstants.ShikimoriIdName => query.Where(a => a.ShikimoriId.ToString().Contains(filter)),
            AnimeStringConstants.ShikimoriScoreName => query.Where(a => a.ShikimoriScore.ToString().Contains(filter)),
            AnimeStringConstants.TmdbScoreName => query.Where(a => a.TmdbScore.ToString().Contains(filter)),
            AnimeStringConstants.ImdbScoreName => query.Where(a => a.ImdbScore.ToString().Contains(filter)),
            AnimeStringConstants.IsPublishedName => query.Where(a => a.IsPublished.ToString().Contains(filter)),
            AnimeStringConstants.PublishedAtName => query.Where(a => a.PublishedAt.HasValue && a.PublishedAt.Value.ToString("yyyy-MM-dd").Contains(filter)),
            AnimeStringConstants.UpdatedAtName => query.Where(a => a.UpdatedAt.ToString("yyyy-MM-dd").Contains(filter)),
            AnimeStringConstants.CreatedAtName => query.Where(a => a.CreatedAt.ToString("yyyy-MM-dd").Contains(filter)),
            _ => query.Where(a => a.Id.ToString().Contains(filter)),
        };
    }

    protected override IQueryable<Anime> Sort(IQueryable<Anime> query, string orderBy, bool isAscending)
    {
        return orderBy switch
        {
            AnimeStringConstants.IdName => isAscending ? query.OrderBy(a => a.Id) : query.OrderByDescending(a => a.Id),
            AnimeStringConstants.NameName => isAscending ? query.OrderBy(a => a.Name) : query.OrderByDescending(a => a.Name),
            AnimeStringConstants.TagsName => isAscending ? query.OrderBy(a => a.Tags.Count) : query.OrderByDescending(a => a.Tags.Count),
            AnimeStringConstants.DubsName => isAscending ? query.OrderBy(a => a.Dubs.Count) : query.OrderByDescending(a => a.Dubs.Count),
            AnimeStringConstants.CountriesName => isAscending ? query.OrderBy(a => a.Countries.Count) : query.OrderByDescending(a => a.Countries.Count),
            AnimeStringConstants.KindName => isAscending ? query.OrderBy(a => a.Kind.Slug) : query.OrderByDescending(a => a.Kind.Slug),
            AnimeStringConstants.StatusName => isAscending ? query.OrderBy(a => a.Status.Name) : query.OrderByDescending(a => a.Status.Name),
            AnimeStringConstants.SourceName => isAscending ? query.OrderBy(a => a.Source.Name) : query.OrderByDescending(a => a.Source.Name),
            AnimeStringConstants.RestrictedRatingName => isAscending ? query.OrderBy(a => a.RestrictedRating.Name) : query.OrderByDescending(a => a.RestrictedRating.Name),
            AnimeStringConstants.FirstAirDateName => isAscending ? query.OrderBy(a => a.FirstAirDate) : query.OrderByDescending(a => a.FirstAirDate),
            AnimeStringConstants.LastAirDateName => isAscending ? query.OrderBy(a => a.LastAirDate) : query.OrderByDescending(a => a.LastAirDate),
            AnimeStringConstants.TmdbIdName => isAscending ? query.OrderBy(a => a.TmdbId) : query.OrderByDescending(a => a.TmdbId),
            AnimeStringConstants.ShikimoriIdName => isAscending ? query.OrderBy(a => a.ShikimoriId) : query.OrderByDescending(a => a.ShikimoriId),
            AnimeStringConstants.ShikimoriScoreName => isAscending ? query.OrderBy(a => a.ShikimoriScore) : query.OrderByDescending(a => a.ShikimoriScore),
            AnimeStringConstants.TmdbScoreName => isAscending ? query.OrderBy(a => a.TmdbScore) : query.OrderByDescending(a => a.TmdbScore),
            AnimeStringConstants.ImdbScoreName => isAscending ? query.OrderBy(a => a.ImdbScore) : query.OrderByDescending(a => a.ImdbScore),
            AnimeStringConstants.IsPublishedName => isAscending ? query.OrderBy(a => a.IsPublished) : query.OrderByDescending(a => a.IsPublished),
            AnimeStringConstants.PublishedAtName => isAscending ? query.OrderBy(a => a.PublishedAt) : query.OrderByDescending(a => a.PublishedAt),
            AnimeStringConstants.UpdatedAtName => isAscending ? query.OrderBy(a => a.UpdatedAt) : query.OrderByDescending(a => a.UpdatedAt),
            AnimeStringConstants.CreatedAtName => isAscending ? query.OrderBy(a => a.CreatedAt) : query.OrderByDescending(a => a.CreatedAt),
            _ => isAscending ? query.OrderBy(a => a.Id) : query.OrderByDescending(a => a.Id),
        };
    }

    protected override void Update(Anime model, Anime entity)
    {
        DbContext.Entry(entity).CurrentValues.SetValues(model);
        entity.SeoAddition = model.SeoAddition;
        entity.Source = model.Source;
        entity.Status = model.Status;
        entity.Kind = model.Kind;
        entity.RestrictedRating = model.RestrictedRating;
        entity.Period = model.Period;
    }
}
