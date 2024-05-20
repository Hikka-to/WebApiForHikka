using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Constants.Models.SeoAdditions;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories;

public class SeoAdditionRepository(HikkaDbContext dbContext) : CrudRepository<SeoAddition>(dbContext), ISeoAdditionRepository
{
    public bool CheckIfTheSeoAdditionExist(Guid id)
    {
        var user = DbContext.Set<SeoAddition>().FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return false;
        }
        return true;
    }

    protected override IQueryable<SeoAddition> Filter(IQueryable<SeoAddition> query, string filterBy, string filter) => filterBy switch
    {
        SeoAdditionStringConstants.DescriptionName => query.Where(m => m.Description.Contains(filter, StringComparison.OrdinalIgnoreCase)),
        SeoAdditionStringConstants.SlugName => query.Where(m => m.Slug.Contains(filter, StringComparison.OrdinalIgnoreCase)),
        SeoAdditionStringConstants.TitleName => query.Where(m => m.Title.Contains(filter, StringComparison.OrdinalIgnoreCase)),
        SeoAdditionStringConstants.ImageName => query.Where(m => m.Image != null && m.Image.Contains(filter, StringComparison.OrdinalIgnoreCase)),
        SeoAdditionStringConstants.ImageAltName => query.Where(m => m.ImageAlt != null && m.ImageAlt.Contains(filter, StringComparison.OrdinalIgnoreCase)),
        SeoAdditionStringConstants.SocialTitleName => query.Where(m => m.SocialTitle != null && m.SocialTitle.Contains(filter, StringComparison.OrdinalIgnoreCase)),
        SeoAdditionStringConstants.SocialTypeName => query.Where(m => m.SocialType != null && m.SocialType.Contains(filter, StringComparison.OrdinalIgnoreCase)),
        SeoAdditionStringConstants.SocialImageName => query.Where(m => m.SocialImage != null && m.SocialImage.Contains(filter, StringComparison.OrdinalIgnoreCase)),
        SeoAdditionStringConstants.SocialImageAltName => query.Where(m => m.SocialImageAlt != null && m.SocialImageAlt.Contains(filter, StringComparison.OrdinalIgnoreCase)),
        _ => query.Where(m => m.Id.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase)),
    };

    protected override IQueryable<SeoAddition> Sort(IQueryable<SeoAddition> query, string orderBy, bool isAscending) => orderBy switch
    {
        SeoAdditionStringConstants.TitleName => isAscending ? query.OrderBy(m => m.Title) : query.OrderByDescending(m => m.Title),
        SeoAdditionStringConstants.DescriptionName => isAscending ? query.OrderBy(m => m.Description) : query.OrderByDescending(m => m.Description),
        SeoAdditionStringConstants.SlugName => isAscending ? query.OrderBy(m => m.Slug) : query.OrderByDescending(m => m.Slug),
        SeoAdditionStringConstants.ImageName => isAscending ? query.OrderBy(m => m.Image ?? "") : query.OrderByDescending(m => m.Image ?? ""),
        SeoAdditionStringConstants.ImageAltName => isAscending ? query.OrderBy(m => m.ImageAlt ?? "") : query.OrderByDescending(m => m.ImageAlt ?? ""),
        SeoAdditionStringConstants.SocialTitleName => isAscending ? query.OrderBy(m => m.SocialTitle ?? "") : query.OrderByDescending(m => m.SocialTitle ?? ""),
        SeoAdditionStringConstants.SocialTypeName => isAscending ? query.OrderBy(m => m.SocialType ?? "") : query.OrderByDescending(m => m.SocialType ?? ""),
        SeoAdditionStringConstants.SocialImageName => isAscending ? query.OrderBy(m => m.SocialImage ?? "") : query.OrderByDescending(m => m.SocialImage ?? ""),
        SeoAdditionStringConstants.SocialImageAltName => isAscending ? query.OrderBy(m => m.SocialImageAlt ?? "") : query.OrderByDescending(m => m.SocialImageAlt ?? ""),
        _ => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id) // Default sorting by Id
    };

    protected override void Update(SeoAddition model, SeoAddition entity)
    {
        entity.Slug = model.Slug;
        entity.Title = model.Title;
        entity.Description = model.Description;
        entity.Image = model.Image;
        entity.ImageAlt = model.ImageAlt;
        entity.SocialTitle = model.SocialTitle;
        entity.SocialType = model.SocialType;
        entity.SocialImage = model.SocialImage;
        entity.SocialImageAlt = model.SocialImageAlt;
    }
}