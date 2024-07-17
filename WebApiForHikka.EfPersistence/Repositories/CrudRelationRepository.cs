using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories;

public abstract class CrudRelationRepository<TModel, TFirstModel, TSecondModel> : CrudRepository<TModel>,
    IRelationCrudRepository<TModel, TFirstModel, TSecondModel>
    where TModel : RelationModel<TFirstModel, TSecondModel>
    where TFirstModel : Model
    where TSecondModel : Model
{
    public CrudRelationRepository(HikkaDbContext dbContext) : base(dbContext)
    {
    }

    public bool CheckIfModelsWithThisIdsExist(Guid firstId, Guid secondId)
    {
        var firstEntity = DbContext.Set<TFirstModel>()
            .FirstOrDefault(e => e.Id == firstId);
        if (firstEntity == null) return false;

        var secondEntity = DbContext.Set<TSecondModel>()
            .FirstOrDefault(e => e.Id == secondId);
        return secondEntity != null;
    }

    public virtual async Task DeleteAsync(Guid firstId, Guid secondId, CancellationToken cancellationToken)
    {
        var entity = await DbContext.Set<TModel>()
            .FirstOrDefaultAsync(e => e.FirstId == firstId && e.SecondId == secondId);

        if (entity is null)
            return;

        DbContext.Set<TModel>().Remove(entity);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public TModel? Get(Guid firstId, Guid secondId)
    {
        return DbContext.Set<TModel>().FirstOrDefault(e => e.FirstId == firstId && e.SecondId == secondId);
    }

    public async Task<TModel?> GetAsync(Guid firstId, Guid secondId, CancellationToken cancellationToken)
    {
        return await DbContext.Set<TModel>().FirstOrDefaultAsync(e => e.FirstId == firstId && e.SecondId == secondId);
    }
}