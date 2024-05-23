using FluentAssertions;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.Test.Shared.Service;

public abstract class SharedServiceTest<TModel, TService>
    : SharedTest
    where TModel : Model
    where TService : ICrudService<TModel>
{
    protected abstract TModel GetSample();
    protected abstract TModel GetSampleForUpdate();
    protected abstract TService GetService(HikkaDbContext hikkaDbContext);

    [Fact]
    public virtual async Task Service_CreateAsync_ReturnsModelAndId()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        TService Service = GetService(dbContext);
        var sample = GetSample();

        // Act
        var result = await Service.CreateAsync(sample, _cancellationToken);

        // Assert
        result.Should().NotBeEmpty();
        var addedStatus = await Service.GetAsync(result, _cancellationToken);
        addedStatus.Should().NotBeNull();
        addedStatus.Should().BeEquivalentTo(sample);
    }
    public virtual async Task Service_Deletesync_DeleteModel()
    {
        // Arrange
        var dbContext =  GetDatabaseContext();
        var service = GetService(dbContext);
        var model = GetSample();

        // Act
        var result = await service.CreateAsync(model, _cancellationToken);

        await service.DeleteAsync(result, _cancellationToken);

        // Assert
        var deletedModel = await service.GetAsync(result, _cancellationToken);
        deletedModel.Should().BeNull();
    }

    [Fact]
    public async virtual Task Repostiroy_GetAllAsync_ReturnsPage()
    {
        // Arrange
        var data = new List<TModel> { GetSample(), GetSample() };
        var dbContext =  GetDatabaseContext();
        var service = GetService(dbContext);
        foreach (var i in data)
        {
            await service.CreateAsync(i, _cancellationToken);
        }
        var dto = new FilterPaginationDto { PageNumber = 1, PageSize = 1 };

        // Act
        var result = await service.GetAllAsync(dto, _cancellationToken);

        // Assert
        Assert.Single(result.Models);
    }



    [Fact]
    public async Task Service_GetAllModelsByIdsAsync_ReturnsModelsByIds()
    {
        // Arrange
        var data = new List<TModel> { GetSample(), GetSample() };
        var dbContext = GetDatabaseContext();

        var service = GetService(dbContext);
        foreach (var i in data)
        {
            await service.CreateAsync(i, _cancellationToken);
        }
        var ids = data.Select(m => m.Id).ToList();

        // Act
        var result = await service.GetAllModelsByIdsAsync(ids, _cancellationToken);

        // Assert
        Assert.Equal(ids.Count, result.Count());
    }
    public async virtual Task Service_GetAsync_ReturnsModel()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var service = GetService(dbContext);
        var sample = GetSample();
        var id = await service.CreateAsync(sample, _cancellationToken);
        sample.Id = id;

        // Act
        var result = await service.GetAsync(id, _cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(sample);
    }

    [Fact]
    public virtual async Task Service_UpdateAsync_UpdateModel()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var service = GetService(dbContext);
        var sample = GetSample();
        var id = await service.CreateAsync(sample, _cancellationToken);
        sample.Id = id;
        var updatedSample = GetSampleForUpdate();
        updatedSample.Id = id;

        // Act
        await service.UpdateAsync(updatedSample, _cancellationToken);

        var result = await service.GetAsync(id, _cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(updatedSample);
    }
}