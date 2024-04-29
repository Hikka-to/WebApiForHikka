﻿
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.Shared;

public interface ICrudRepository<TModel> where TModel : Model
{
    Task<Guid> AddAsync(TModel model, CancellationToken cancellationToken);
    Task UpdateAsync(TModel model, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<TModel?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<PaginatedCollection<TModel>> GetAllAsync(FilterPaginationDto dto, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<TModel>> GetAllAsync(CancellationToken cancellationToken);
    Task<IReadOnlyCollection<TModel?>> GetAllModelsByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
   
}
