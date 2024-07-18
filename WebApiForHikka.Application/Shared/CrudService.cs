using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.Shared;

public abstract class CrudService<TModel, TRepository> : ICrudService<TModel> where TModel : class, IModel
    where TRepository : ICrudRepository<TModel>
{
    protected readonly TRepository RelationRepository;

    public CrudService(TRepository relationRepository)
    {
        RelationRepository = relationRepository;
    }

    public virtual async Task<Guid> CreateAsync(TModel model, CancellationToken cancellationToken)
    {
        return await RelationRepository.AddAsync(model, cancellationToken);
    }

    public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await RelationRepository.DeleteAsync(id, cancellationToken);
    }

    public virtual async Task<PaginatedCollection<TModel>> GetAllAsync(FilterPagination dto,
        CancellationToken cancellationToken)
    {
        return await RelationRepository.GetAllAsync(dto, cancellationToken);
    }

    public virtual async Task<TModel?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await RelationRepository.GetAsync(id, cancellationToken);
    }

    public virtual async Task<IReadOnlyCollection<TModel?>> GetAllModelsByIdsAsync(List<Guid> ids,
        CancellationToken cancellationToken)
    {
        return await RelationRepository.GetAllModelsByIdsAsync(ids, cancellationToken);
    }


    public virtual async Task UpdateAsync(TModel model, CancellationToken cancellationToken)
    {
        await RelationRepository.UpdateAsync(model, cancellationToken);
    }

    public TModel? Get(Guid id)
    {
        return RelationRepository.Get(id);
    }
}