using FluentAssertions;
using WebApiForHikka.Application.Sources;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.ModelsWithSeoAddition;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared;

namespace WebApiForHikka.Test.Service.Sources;

public class SourceServiceTest : SharedTest
{
    private Source GetSourceSample()
    {
        return new Source()
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
    public async Task SourceService_CreateAsync_ReturnsSource()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var sourceRepository = new SourceRepository(dbContext);
        var sourceService = new SourceService(sourceRepository);

        var sourceSample = GetSourceSample();

        // Act
        var id = await sourceService.CreateAsync(sourceSample, new CancellationToken());

        var result = await sourceService.GetAsync(id, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(sourceSample);
    }

    [Fact]
    public async Task SourceService_DeleteAsync_DeletesSource()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var sourceRepository = new SourceRepository(dbContext);
        var sourceService = new SourceService(sourceRepository);

        var sourceSample = GetSourceSample();
        var id = await sourceService.CreateAsync(sourceSample, new CancellationToken());

        // Act
        await sourceService.DeleteAsync(id, new CancellationToken());
        var result = await sourceService.GetAsync(id, new CancellationToken());

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task SourceService_UpdateAsync_UpdatesSource()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var sourceRepository = new SourceRepository(dbContext);
        var sourceService = new SourceService(sourceRepository);
        var sourceSample = GetSourceSample();
        var id = await sourceService.CreateAsync(sourceSample, new CancellationToken());
        sourceSample.Id = id;
        var updatedSourceSample = new Source
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
        await sourceService.UpdateAsync(updatedSourceSample, new CancellationToken());

        var result = await sourceService.GetAsync(id, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(updatedSourceSample);
    }
}
