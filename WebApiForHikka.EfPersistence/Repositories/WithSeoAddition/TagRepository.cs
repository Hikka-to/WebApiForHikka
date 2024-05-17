using SushiRestaurant.EfPersistence.Repositories;
using WebApiForHikka.Application.WithSeoAddition.Tags;
using WebApiForHikka.Constants.Models.Tags;
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
            TagStringConstants.NameName => query.Where(m => m.Name.Contains(filter, StringComparison.OrdinalIgnoreCase)),
            TagStringConstants.AlisesName => query.Where(m => m.Alises.Contains(filter, StringComparison.OrdinalIgnoreCase)),
            TagStringConstants.IsGenreName => query.Where(m => m.IsGenre.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase)),
            TagStringConstants.EngNameName => query.Where(m => m.EngName.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase)),
            TagStringConstants.ParentTagName => query.Where(m => m.ParentTag != null && m.ParentTag.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase)),
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
            TagStringConstants.ParentTagName => isAscending ? query.OrderBy(m => m.ParentTag) : query.OrderByDescending(m => m.Name),
            _ => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id)
        };
    }

    protected override void Update(Tag model, Tag entity)
    {
        entity.ParentTag = model.ParentTag;
        entity.IsGenre = model.IsGenre;
        entity.SeoAddition = model.SeoAddition;
        entity.Alises = model.Alises;
        entity.EngName = model.EngName;
        entity.Name = model.Name;
    }
}
