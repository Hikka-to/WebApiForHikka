using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.Shared.Relation;

public interface IRelationCrudRepository<TModel, TFirstModel, TSecondModel> : ICrudRepository<TModel>
    where TModel : RelationModel<TFirstModel, TSecondModel>
    where TFirstModel : Model
    where TSecondModel : Model
{
    Task DeleteAsync(Guid firstId, Guid secondId, CancellationToken cancellationToken);
    Task<TModel?> GetAsync(Guid firstId, Guid secondId, CancellationToken cancellationToken);
    TModel? Get(Guid firstId, Guid secondId);

}
