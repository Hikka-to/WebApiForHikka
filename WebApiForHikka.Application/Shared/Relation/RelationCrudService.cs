using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.Shared.Relation;

public abstract class RelationCrudService<TModel, TFirstModel, TSecondModel, TRepository>
    : CrudService<TModel, TRepository>, IRelationCrudService<TModel, TFirstModel, TSecondModel>
    where TModel : RelationModel<TFirstModel, TSecondModel>
    where TRepository : IRelationCrudRepository<TModel, TFirstModel, TSecondModel>
    where TFirstModel : class, IModel
    where TSecondModel : class, IModel
{
    protected RelationCrudService(TRepository relationRepository) : base(relationRepository)
    {
    }

    public bool CheckIfModelsWithThisIdsExist(Guid firstId, Guid secondId)
    {
        return _repository.CheckIfModelsWithThisIdsExist(firstId, secondId);
    }

    public async Task DeleteAsync(Guid firstId, Guid secondId, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(firstId, secondId, cancellationToken);
    }

    public TModel? Get(Guid firstId, Guid secondId)
    {
        return _repository.Get(firstId, secondId);
    }

    public async Task<TModel?> GetAsync(Guid firstId, Guid secondId, CancellationToken cancellationToken)
    {
        return await _repository.GetAsync(firstId, secondId, cancellationToken);
    }
}