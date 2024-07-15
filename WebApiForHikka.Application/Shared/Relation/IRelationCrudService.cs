﻿using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.Shared.Relation;

public interface IRelationCrudService<TModel, TFirstModel, TSecondModel> : ICrudService<TModel>
    where TModel : RelationModel<TFirstModel, TSecondModel>
    where TFirstModel : class, IModel
    where TSecondModel : class, IModel
{
    Task DeleteAsync(Guid firstId, Guid secondId, CancellationToken cancellationToken);
    Task<TModel?> GetAsync(Guid firstId, Guid secondId, CancellationToken cancellationToken);
    TModel? Get(Guid firstId, Guid secondId);

    bool CheckIfModelsWithThisIdsExist(Guid firstId, Guid secondId);
}