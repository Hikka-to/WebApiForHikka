using FluentAssertions;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.Test.Shared.Repository;

public abstract class SharedRepositoryTest<TModel, TRepository> 
    : SharedTest
    where TModel : Model
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
        var result = await Repository.AddAsync(sample, _cancellationToken);

        // Assert
        result.Should().NotBeEmpty();
        var addedStatus = await Repository.GetAsync(result, _cancellationToken);
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
        var result = await repository.AddAsync(model, _cancellationToken);

        await repository.DeleteAsync(result, _cancellationToken);

        // Assert
        var deletedModel = await repository.GetAsync(result, _cancellationToken);
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
            await repository.AddAsync(i, _cancellationToken);
        }
        var dto = new FilterPaginationDto { PageNumber = 1, PageSize = 1 };

        // Act
        var result = await repository.GetAllAsync(dto, _cancellationToken);

        // Assert
        Assert.Single(result.Models);
    }

    [Fact]
    public async Task Repository_GetAllAsync_ReturnsAllModels()
    {
        // Arrange
        var data = new List<TModel> { GetSample(), GetSample() };
        var dbContext =  GetDatabaseContext();
        var repository = GetRepository(dbContext);

        foreach (var i in data)
        {
            await repository.AddAsync(i, _cancellationToken);
        }
        var ids = data.Select(m => m.Id).ToList();

        // Act
        var result = await repository.GetAllAsync(_cancellationToken);

        // Assert
        Assert.Equal(ids.Count, result.Count);
    }



    [Fact]
    public async Task Repository_GetAllModelsByIdsAsync_ReturnsModelsByIds()
    {
        // Arrange
        var data = new List<TModel> { GetSample(), GetSample() };
        var dbContext =  GetDatabaseContext();

        var repository = GetRepository(dbContext);
        foreach (var i in data)
        {
            await repository.AddAsync(i, _cancellationToken);
        }
        var ids = data.Select(m => m.Id).ToList();

        // Act
        var result = await repository.GetAllModelsByIdsAsync(ids, _cancellationToken);

        // Assert
        Assert.Equal(ids.Count, result.Count);
    }
    public async virtual Task Repository_GetAsync_ReturnsModel()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var repository = GetRepository(dbContext);
        var sample = GetSample();
        var id = await repository.AddAsync(sample, _cancellationToken);
        sample.Id = id;

        // Act
        var result = await repository.GetAsync(id, _cancellationToken);

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
        var id = await repository.AddAsync(sample, _cancellationToken);
        sample.Id = id;
        var updatedSample = GetSampleForUpdate();
        updatedSample.Id = id;

        // Act
        await repository.UpdateAsync(updatedSample, _cancellationToken);

        var result = await repository.GetAsync(id, _cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(updatedSample);
    }

    public async virtual Task Repository_Get_ReturnsModel()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var repository = GetRepository(dbContext);
        var sample = GetSample();
        var id = await repository.AddAsync(sample, _cancellationToken);
        sample.Id = id;

        // Act
        var result = repository.Get(id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(sample);
    }

}