using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.Shared.Relation;

public interface IRelationCrudService <TModel> : ICrudService<TModel> where TModel : class, IModel
{

    Task DeleteAsync(Guid firstId, Guid secondId, CancellationToken cancellationToken);
    Task<TModel?> GetAsync(Guid firstId, Guid secondId, CancellationToken cancellationToken);
    TModel? Get(Guid firstId, Guid secondId);
    Task<TModel?> Get(Guid firstId, Guid secondId, CancellationToken cancellationToken);


}
