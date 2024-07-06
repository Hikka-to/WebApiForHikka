using FluentAssertions;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;

namespace WebApiForHikka.Test.Shared.Repository;

public abstract class SharedRelationRepositoryTest<
    TRelationModel, TFirstModel, TSecondModel,
    TRelationRepository, TFirstRepository, TSecondRepository
    > : SharedRepositoryTest<TRelationModel, TRelationRepository>
    where TRelationModel : RelationModel<TFirstModel, TSecondModel>
    where TRelationRepository : CrudRelationRepository<TRelationModel, TFirstModel, TSecondModel>, IRelationCrudRepository<TRelationModel, TFirstModel, TSecondModel>
    where TFirstRepository : class, ICrudRepository<TFirstModel>
    where TSecondRepository : class, ICrudRepository<TSecondModel>
    where TFirstModel : Model
    where TSecondModel : Model
{



    protected abstract (TFirstRepository firstRepository, TSecondRepository secondRepository) GetFirstAndSecondRepositories(HikkaDbContext hikkaDbContext);
    protected abstract Task<(Guid firstId, Guid secondId)> CreateFirstAndSecondModels((TFirstRepository firstRepository, TSecondRepository secondRepository) repostiroeis);

    protected abstract TFirstModel GetFirstModelSample();
    protected abstract TSecondModel GetSecondModelSample();

    protected abstract TRelationModel GetRelationModel(Guid firstId, Guid secondId);


    [Fact]
    public virtual async Task RepositoryRelation_DeleteByTwoIdsAsync_DeleteModel()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var firstAndSecondRepositories = GetFirstAndSecondRepositories(dbContext);
        TRelationRepository Repository = GetRepository(dbContext);
        (Guid firstId, Guid secondId) sampleIds = await CreateFirstAndSecondModels(firstAndSecondRepositories);

        var RelationModelId = await Repository.AddAsync(GetRelationModel(sampleIds.firstId, sampleIds.secondId), CancellationToken);


        // Act

        var addedEntety = await Repository.GetAsync(RelationModelId, CancellationToken);


        await Repository.DeleteAsync(sampleIds.firstId, sampleIds.secondId, CancellationToken);


        var result = await Repository.GetAsync(RelationModelId, CancellationToken);


        // Assert
        addedEntety.Should().NotBeNull();
        result.Should().BeNull();
    }

    [Fact]
    public virtual async Task RepositoryRelation_GetAsync_ReturnsModel()
    {

    }

    [Fact]
    public virtual async Task RepositoryRelation_Get_ReturnModel()
    {

    }

}
