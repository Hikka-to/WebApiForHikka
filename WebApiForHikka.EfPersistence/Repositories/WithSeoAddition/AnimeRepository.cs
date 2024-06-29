using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Constants.Models.Animes;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;

public class AnimeRepository(HikkaDbContext dbContext) : CrudRepository<Anime>(dbContext), IAnimeRepository
{
    protected override IQueryable<Anime> Filter(IQueryable<Anime> query, string filterBy, string filter)
    {
        return filterBy switch
        {
            AnimeStringConstants.IdName => query.Where(a => a.Id.ToString().Contains(filter)),
            AnimeStringConstants.TagsName => query.Where(a => a.Tags.Any(t => EF.Functions.ILike(t.Name, $"%{filter}%"))),
            AnimeStringConstants.CountriesName => query.Where(a => a.Countries.Any(c => EF.Functions.ILike(c.Name, $"%{filter}%"))),
            AnimeStringConstants.DubsName => query.Where(a => a.Dubs.Any(d => EF.Functions.ILike(d.Name, $"%{filter}%"))),
            AnimeStringConstants.KindName => query.Where(a => EF.Functions.ILike(a.Kind.Slug, $"%{filter}%")),
            AnimeStringConstants.StatusName => query.Where(a => EF.Functions.ILike(a.Status.Name, $"%{filter}%")),
            AnimeStringConstants.PeriodName => query.Where(a => EF.Functions.ILike(a.Period.Name, $"%{filter}%")),
            AnimeStringConstants.RestrictedRatingName => query.Where(a => EF.Functions.ILike(a.RestrictedRating.Name, $"%{filter}%")),
            AnimeStringConstants.SourceName => query.Where(a => EF.Functions.ILike(a.Source.Name, $"%{filter}%")),
            AnimeStringConstants.NameName => query.Where(a => EF.Functions.ILike(a.Name, $"%{filter}%")),
            AnimeStringConstants.ImageNameName => query.Where(a => a.ImageName != null && EF.Functions.ILike(a.ImageName, $"%{filter}%")),
            AnimeStringConstants.RomajiNameName => query.Where(a => a.RomajiName != null && EF.Functions.ILike(a.RomajiName, $"%{filter}%")),
            AnimeStringConstants.NativeNameName => query.Where(a => EF.Functions.ILike(a.NativeName, $"%{filter}%")),
            AnimeStringConstants.PosterPathName => query.Where(a => EF.Functions.ILike(a.PosterPath, $"%{filter}%")),
            AnimeStringConstants.PosterColorsName => query.Where(a => a.PosterColors.Any(pc => pc.ToString().Contains(filter))),
            AnimeStringConstants.AvgDurationName => query.Where(a => a.AvgDuration.ToString().Contains(filter)),
            AnimeStringConstants.HowManyEpisodesName => query.Where(a => a.HowManyEpisodes.ToString().Contains(filter)),
            AnimeStringConstants.FirstAirDateName => query.Where(a => a.FirstAirDate.ToString("yyyy-MM-dd").Contains(filter)),
            AnimeStringConstants.LastAirDateName => query.Where(a => a.LastAirDate.ToString("yyyy-MM-dd").Contains(filter)),
            AnimeStringConstants.TmdbIdName => query.Where(a => a.TmdbId != null && a.TmdbId.ToString()!.Contains(filter)),
            AnimeStringConstants.ShikimoriIdName => query.Where(a => a.ShikimoriId != null && a.ShikimoriId.ToString()!.Contains(filter)),
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
