using FluentAssertions;
using WebApiForHikka.Application.Kinds;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared;

namespace WebApiForHikka.Test.Service.Kinds;

public class KindServiceTest : SharedTest
{
    public Kind GetKindSample()
    {
        return new Kind()
        {
            Hint = "Test",
            Slug = "Test",
            SeoAddition = new SeoAddition()
            {
                Description = "Test",
                Slug = "Test",
                Title = "Test",
            },
        };
    }

    [Fact]
    public async Task KindService_CreateAsync_ReturnsKind()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var kindRepository = new KindRepository(dbContext);
        var kindService = new KindService(kindRepository);

        var kindSample = GetKindSample();

        // Act
        var id = await kindService.CreateAsync(kindSample, new CancellationToken());

        var result = await kindService.GetAsync(id, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(kindSample);
    }

    [Fact]
    public async Task KindService_DeleteAsync_DeletesKind()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var kindRepository = new KindRepository(dbContext);
        var kindService = new KindService(kindRepository);

        var kindSample = GetKindSample();
        var id = await kindService.CreateAsync(kindSample, new CancellationToken());

        // Act
        await kindService.DeleteAsync(id, new CancellationToken());
        var result = await kindService.GetAsync(id, new CancellationToken());

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task KindService_UpdateAsync_UpdatesKind()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var kindRepository = new KindRepository(dbContext);
        var kindService = new KindService(kindRepository);
        var kindSample = GetKindSample();
        var id = await kindService.CreateAsync(kindSample, new CancellationToken());
        kindSample.Id = id;
        var updatedKindSample = new Kind
        {
            Id = id,
            Hint = "Test1",
            Slug = "Test1",
            SeoAddition = new SeoAddition
            {
                Description = "Test1",
                Slug = "Test1",
                Title = "Test1",
            }
        };

        // Act
        await kindService.UpdateAsync(updatedKindSample, new CancellationToken());

        var result = await kindService.GetAsync(id, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(updatedKindSample);
    }
}