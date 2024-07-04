using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.Shared.Relation;

public abstract class RelationCrudService<TModel, TRepository> : CrudService<TModel, TRepository>,  IRelationCrudService<TModel> where TModel :  RelationModel where TRepository : IRelationCrudRepository<TModel>
{
    protected RelationCrudService(TRepository repository) : base(repository)
    {

    }

    public Task DeleteAsync(Guid firstId, Guid secondId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public TModel? Get(Guid firstId, Guid secondId)
    {
        throw new NotImplementedException();
    }

    public Task<TModel?> Get(Guid firstId, Guid secondId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<TModel?> GetAsync(Guid firstId, Guid secondId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
