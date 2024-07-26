using FluentAssertions;
using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Test.Shared.Service;

public abstract class SharedRelationServiceTest<TRelationModel, TService, TFirstModel, TSecondModel>
    : SharedServiceTest<TRelationModel, TService>
    where TRelationModel : RelationModel<TFirstModel, TSecondModel>
    where TService : IRelationCrudService<TRelationModel, TFirstModel, TSecondModel>
    where TFirstModel : class, IModel
    where TSecondModel : class, IModel
{
    [Fact]
    public virtual async Task RelationService_DeleteAsync_DeleteModel()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var service = GetService(dbContext);
        var sample = GetSample();

        // Act
        await service.CreateAsync(sample, CancellationToken);

        await service.DeleteAsync(sample.FirstId, sample.SecondId, CancellationToken);

        // Assert
        var deletedModel = await service.GetAsync(sample.FirstId, sample.SecondId, CancellationToken);
        deletedModel.Should().BeNull();
    }

    [Fact]
    public virtual async Task RelationService_GetAsync_ReturnsModel()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var service = GetService(dbContext);
        var sample = GetSample();
        var id = await service.CreateAsync(sample, CancellationToken);
        sample.Id = id;

        // Act
        var addedModel = await service.GetAsync(sample.FirstId, sample.SecondId, CancellationToken);

        // Assert
        addedModel.Should().NotBeNull();
        addedModel.Should().BeEquivalentTo(sample);
    }

    [Fact]
    public virtual async Task RelationService_CheckIfModelsWithThisIdsExist_ReturnsTrue()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var service = GetService(dbContext);
        var sample = GetSample();
        var id = await service.CreateAsync(sample, CancellationToken);
        var (firstId, secondId) = (await service.GetAsync(id, CancellationToken))!;

        // Act
        var result = service.CheckIfModelsWithThisIdsExist(firstId, secondId);

        // Assert
        result.Should().BeTrue();
    }

    public override async Task Service_UpdateAsync_UpdateModel()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var service = GetService(dbContext);
        var sample = GetSample();
        var id = await service.CreateAsync(sample, CancellationToken);
        sample.Id = id;
        var updatedSample = GetSampleForUpdate();
        updatedSample.Id = id;

        // Act
        await service.UpdateAsync(updatedSample, CancellationToken);

        var result = await service.GetAsync(id, CancellationToken);

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