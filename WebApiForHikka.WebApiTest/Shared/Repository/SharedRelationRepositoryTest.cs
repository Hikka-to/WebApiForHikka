using FluentAssertions;
using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Test.Shared.Repository;

public abstract class SharedRelationRepositoryTest<
    TRelationModel, TFirstModel, TSecondModel,
    TRelationRepository
> : SharedRepositoryTest<TRelationModel, TRelationRepository>
    where TRelationModel : RelationModel<TFirstModel, TSecondModel>
    where TRelationRepository : IRelationCrudRepository<TRelationModel, TFirstModel, TSecondModel>
    where TFirstModel : class, IModel
    where TSecondModel : class, IModel
{
    [Fact]
    public virtual async Task RelationService_DeleteAsync_DeleteModel()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var repository = GetRepository(dbContext);
        var sample = GetSample();

        // Act
        await repository.AddAsync(sample, CancellationToken);

        await repository.DeleteAsync(sample.FirstId, sample.SecondId, CancellationToken);

        // Assert
        var deletedModel = await repository.GetAsync(sample.FirstId, sample.SecondId, CancellationToken);
        deletedModel.Should().BeNull();
    }

    [Fact]
    public virtual async Task RelationService_GetAsync_ReturnsModel()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var repository = GetRepository(dbContext);
        var sample = GetSample();
        var id = await repository.AddAsync(sample, CancellationToken);
        sample.Id = id;

        // Act
        var addedModel = await repository.GetAsync(sample.FirstId, sample.SecondId, CancellationToken);

        // Assert
        addedModel.Should().NotBeNull();
        addedModel.Should().BeEquivalentTo(sample);
    }

    [Fact]
    public virtual async Task RelationService_CheckIfModelsWithThisIdsExist_ReturnsTrue()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var repository = GetRepository(dbContext);
        var sample = GetSample();
        var id = await repository.AddAsync(sample, CancellationToken);
        var (firstId, secondId) = (await repository.GetAsync(id, CancellationToken))!;

        // Act
        var result = repository.CheckIfModelsWithThisIdsExist(firstId, secondId);

        // Assert
        result.Should().BeTrue();
    }

    public override async Task Repository_UpdateAsync_UpdateModel()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var repository = GetRepository(dbContext);
        var sample = GetSample();
        var id = await repository.AddAsync(sample, CancellationToken);
        sample.Id = id;
        var updatedSample = GetSampleForUpdate();
        updatedSample.Id = id;

        // Act
        await repository.UpdateAsync(updatedSample, CancellationToken);

        var result = await repository.GetAsync(id, CancellationToken);

        updatedSample.FirstId = sample.FirstId;
        updatedSample.SecondId = sample.SecondId;

        if (typeof(TRelationModel).GetProperty("UpdatedAt") is { } updateProperty)
            updateProperty.SetValue(updatedSample, updateProperty.GetValue(result));

        if (typeof(TRelationModel).GetProperty("CreatedAt") is { } createProperty)
            createProperty.SetValue(updatedSample, createProperty.GetValue(result));

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(updatedSample);
    }
}