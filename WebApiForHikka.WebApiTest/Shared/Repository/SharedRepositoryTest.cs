using FluentAssertions;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.Test.Shared.Repository;

public abstract class SharedRepositoryTest<TModel, TRepository>
    : SharedTest
    where TModel : IModel
    where TRepository : ICrudRepository<TModel>
{
    protected abstract TModel GetSample();
    protected abstract TModel GetSampleForUpdate();
    protected abstract TRepository GetRepository(HikkaDbContext hikkaDbContext);


    [Fact]
    public virtual async Task Repository_AddAsync_ReturnsModelAndId()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        TRepository Repository = GetRepository(dbContext);
        var sample = GetSample();

        // Act
        var result = await Repository.AddAsync(sample, CancellationToken);

        // Assert
        result.Should().NotBeEmpty();
        var addedStatus = await Repository.GetAsync(result, CancellationToken);
        addedStatus.Should().NotBeNull();
        addedStatus.Should().BeEquivalentTo(sample);
    }
    public virtual async Task Repository_Deletesync_DeleteModel()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var repository = GetRepository(dbContext);
        var model = GetSample();

        // Act
        var result = await repository.AddAsync(model, CancellationToken);

        await repository.DeleteAsync(result, CancellationToken);

        // Assert
        var deletedModel = await repository.GetAsync(result, CancellationToken);
        deletedModel.Should().BeNull();
    }

    [Fact]
    public async virtual Task Repository_GetAllAsync_ReturnsPage()
    {
        // Arrange
        var data = new List<TModel> { GetSample(), GetSample() };
        var dbContext = GetDatabaseContext();
        var repository = GetRepository(dbContext);
        foreach (var i in data)
        {
            await repository.AddAsync(i, CancellationToken);
        }
        var dto = new FilterPaginationDto { PageNumber = 1, PageSize = 1 };

        // Act
        var result = await repository.GetAllAsync(dto, CancellationToken);

        // Assert
        Assert.Single(result.Models);
    }

    [Fact]
    public async Task Repository_GetAllAsync_ReturnsAllModels()
    {
        // Arrange
        var data = new List<TModel> { GetSample(), GetSample() };
        var dbContext = GetDatabaseContext();
        var repository = GetRepository(dbContext);

        foreach (var i in data)
        {
            await repository.AddAsync(i, CancellationToken);
        }
        var ids = data.Select(m => m.Id).ToList();

        // Act
        var result = await repository.GetAllAsync(CancellationToken);

        // Assert
        Assert.Equal(ids.Count, result.Count);
    }



    [Fact]
    public async Task Repository_GetAllModelsByIdsAsync_ReturnsModelsByIds()
    {
        // Arrange
        var data = new List<TModel> { GetSample(), GetSample() };
        var dbContext = GetDatabaseContext();

        var repository = GetRepository(dbContext);
        foreach (var i in data)
        {
            await repository.AddAsync(i, CancellationToken);
        }
        var ids = data.Select(m => m.Id).ToList();

        // Act
        var result = await repository.GetAllModelsByIdsAsync(ids, CancellationToken);

        // Assert
        Assert.Equal(ids.Count, result.Count);
    }
    public async virtual Task Repository_GetAsync_ReturnsModel()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var repository = GetRepository(dbContext);
        var sample = GetSample();
        var id = await repository.AddAsync(sample, CancellationToken);
        sample.Id = id;

        // Act
        var result = await repository.GetAsync(id, CancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(sample);
    }

    [Fact]
    public virtual async Task Repository_UpdateAsync_UpdateModel()
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

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(updatedSample);
    }
}