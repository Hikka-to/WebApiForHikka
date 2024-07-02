using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.WithSeoAddition.Tags;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;

public class TagRepository(HikkaDbContext dbContext) : CrudRepository<Tag>(dbContext), ITagRepository
{
    public override async Task<IReadOnlyCollection<Tag>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Set<Tag>().IgnoreAutoIncludes().Include(e => e.ParentTag).ToArrayAsync(cancellationToken);
    }
}

