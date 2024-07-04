using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.Shared.Relation;

public interface IRelationCrudRepository<TModel> : ICrudRepository<TModel> where TModel : RelationModel
{
    Task DeleteAsync(Guid firstId, Guid secondId, CancellationToken cancellationToken);
    Task<TModel?> GetAsync(Guid firstId, Guid secondId, CancellationToken cancellationToken);
    TModel? Get(Guid firstId, Guid secondId);

}
