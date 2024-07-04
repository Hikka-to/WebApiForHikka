using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace WebApiForHikka.EfPersistence.Repositories;
public class CrudRelationRepository<TModel, TFirstModel, TSecondModel> : CrudRepository<TModel>, IRelationCrudRepository<TModel, TFirstModel, TSecondModel>
    where TModel : RelationModel<TFirstModel, TSecondModel>
    where TFirstModel : Model
    where TSecondModel : Model
{
    public CrudRelationRepository(HikkaDbContext dbContext) : base(dbContext)
    {
    }

    public virtual async Task DeleteAsync(Guid firstId, Guid secondId, CancellationToken cancellationToken)
    {


        var entity = await DbContext.Set<TModel>().FirstOrDefaultAsync(e => e.Id == firstId && e.SecondId == secondId);

        if (entity is null)
            return;

        DbContext.Set<TModel>().Remove(entity);
        await DbContext.SaveChangesAsync(cancellationToken);

    }

    public TModel? Get(Guid firstId, Guid secondId)
    {
        return DbContext.Set<TModel>().FirstOrDefault(e => e.Id == firstId && e.SecondId == secondId);
    }

    public async Task<TModel?> GetAsync(Guid firstId, Guid secondId, CancellationToken cancellationToken)
    {
        return await DbContext.Set<TModel>().FirstOrDefaultAsync(e => e.Id == firstId && e.SecondId == secondId);
    }

}
