using FluentAssertions;
using WebApiForHikka.Application.Statuses;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.ModelsWithSeoAddition;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared;

namespace WebApiForHikka.Test.Repository.Statuses;

public class StatusRepositoryTest : SharedTest
{
    private Status GetStatusSample()
    {
        return new Status()
        {
            Name = "Test",
            SeoAddition = new SeoAddition()
            {
                Description = "Test",
                Slug = "Test",
                Title = "Test",
                Image = "Test",
                ImageAlt = "Test",
                SocialImage = "Test",
                SocialImageAlt = "Test",
            }
        };
    }

    [Fact]
    public async Task StatusRepository_AddSync_ReturnsStatusAndId()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var statusRepository = new StatusRepository(dbContext);
        var testStatus = GetStatusSample();

        // Act
        var result = await statusRepository.AddAsync(testStatus, new CancellationToken());

        // Assert
        result.Should().NotBeEmpty();
        var addedStatus = await statusRepository.GetAsync(result, new CancellationToken());
        addedStatus.Should().NotBeNull();
        addedStatus.Should().BeEquivalentTo(testStatus);
    }

    [Fact]
    public async Task StatusService_UpdateAsync_UpdatesStatus()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var statusRepository = new StatusRepository(dbContext);
        var statusSample = GetStatusSample();
        var id = await statusRepository.AddAsync(statusSample, new CancellationToken());
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
        await statusRepository.UpdateAsync(updatedStatusSample, new CancellationToken());

        var result = await statusRepository.GetAsync(id, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(updatedStatusSample);
    }

    [Fact]
    public async Task StatusRepository_DeleteSync_DeleteStatus()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var statusRepository = new StatusRepository(dbContext);
        var testStatus = GetStatusSample();

        // Act
        var result = await statusRepository.AddAsync(testStatus, new CancellationToken());

        await statusRepository.DeleteAsync(result, new CancellationToken());

        // Assert
        var deletedStatus = await statusRepository.GetAsync(result, new CancellationToken());
        deletedStatus.Should().BeNull();
    }
}
