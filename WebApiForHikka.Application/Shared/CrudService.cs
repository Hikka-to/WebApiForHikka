using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.Shared;

public abstract class CrudService<TModel, TRepository> : ICrudService<TModel> where TModel : class, IModel
    where TRepository : ICrudRepository<TModel>
{
    protected readonly TRepository _repository;

    public CrudService(TRepository relationRepository)
    {
        _repository = relationRepository;
    }

    public virtual async Task<Guid> CreateAsync(TModel model, CancellationToken cancellationToken)
    {
        return await _repository.AddAsync(model, cancellationToken);
    }

    public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(id, cancellationToken);
    }

    public virtual async Task<PaginatedCollection<TModel>> GetAllAsync(FilterPagination dto,
        CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(dto, cancellationToken);
    }

    public virtual async Task<TModel?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.GetAsync(id, cancellationToken);
    }

    public virtual async Task<IReadOnlyCollection<TModel?>> GetAllModelsByIdsAsync(List<Guid> ids,
        CancellationToken cancellationToken)
    {
        return await _repository.GetAllModelsByIdsAsync(ids, cancellationToken);
    }


    public virtual async Task UpdateAsync(TModel model, CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(model, cancellationToken);
    }

    public TModel? Get(Guid id)
    {
        return _repository.Get(id);
    }
}