using FluentAssertions;
using WebApiForHikka.Application.Statuses;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared;

namespace WebApiForHikka.Test.Service.Statuses;

public class StatusServiceTest : SharedTest
{
    public Status GetStatusSample()
    {
        return new Status()
        {
            Name = "Test",
            SeoAddition = new SeoAddition
            {
                Description = "test",
                Slug = "test",
                Title = "test",
                Image = "test",
                ImageAlt = "test",
                SocialImage = "test",
                SocialImageAlt = "test",
                SocialTitle = "test",
                SocialType = "test",
            }
        };
    }

    [Fact]
    public async Task StatusService_CreateAsync_ReturnsStatus()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var statusRepository = new StatusRepository(dbContext);
        var statusService = new StatusService(statusRepository);

        var statusSample = GetStatusSample();

        // Act
        var id = await statusService.CreateAsync(statusSample, new CancellationToken());

        var result = await statusService.GetAsync(id, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(statusSample);
    }

    [Fact]
    public async Task StatusService_DeleteAsync_DeletesStatus()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var statusRepository = new StatusRepository(dbContext);
        var statusService = new StatusService(statusRepository);

        var statusSample = GetStatusSample();
        var id = await statusService.CreateAsync(statusSample, new CancellationToken());

        // Act
        await statusService.DeleteAsync(id, new CancellationToken());
        var result = await statusService.GetAsync(id, new CancellationToken());

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task StatusService_UpdateAsync_UpdatesStatus()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var statusRepository = new StatusRepository(dbContext);
        var statusService = new StatusService(statusRepository);
        var statusSample = GetStatusSample();
        var id = await statusService.CreateAsync(statusSample, new CancellationToken());
        statusSample.Id = id;
        var updatedStatusSample = new Status
        {
            Id = id,
            Name = "Test1",
            SeoAddition = new SeoAddition
            {
                Description = "test1",
                Slug = "test1",
                Title = "test1",
                Image = "test1",
                ImageAlt = "test1",
                SocialImage = "test1",
                SocialImageAlt = "test1",
                SocialTitle = "test1",
                SocialType = "test1",
            }
        };

        // Act
        await statusService.UpdateAsync(updatedStatusSample, new CancellationToken());

        var result = await statusService.GetAsync(id, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(updatedStatusSample);
    }
}
