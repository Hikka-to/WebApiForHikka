using FluentAssertions;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.ModelsWithSeoAddition;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared;

namespace WebApiForHikka.Test.Repository.Sources;

public class SourceRepositoryTest : SharedTest
{
    private Source GetSourceSample()
    {
        return new Source()
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
    public async Task SourceRepository_AddSync_ReturnsSourceAndId()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var sourceRepository = new SourceRepository(dbContext);
        var testSource = GetSourceSample();

        // Act
        var result = await sourceRepository.AddAsync(testSource, new CancellationToken());

        // Assert
        result.Should().NotBeEmpty();
        var addedSource = await sourceRepository.GetAsync(result, new CancellationToken());
        addedSource.Should().NotBeNull();
        addedSource.Should().BeEquivalentTo(testSource);
    }

    [Fact]
    public async Task SourceRepository_UpdateAsync_UpdatesSource()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var sourceRepository = new SourceRepository(dbContext);
        var testSource = GetSourceSample();
        var id = await sourceRepository.AddAsync(testSource, new CancellationToken());
        testSource.Id = id;

        // Prepare the updated source
        var updatedSource = new Source
        {
            Id = id,
            Name = "Test1",
            SeoAddition = new SeoAddition
            {
                Description = "Test1",
                Slug = "Test1",
                Title = "Test1",
                Image = "Test1",
                ImageAlt = "Test1",
                SocialImage = "Test1",
                SocialImageAlt = "Test1"
            }
        };

        // Act
        await sourceRepository.UpdateAsync(updatedSource, new CancellationToken());

        var result = await sourceRepository.GetAsync(id, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(updatedSource);
    }


    [Fact]
    public async Task SourceRepository_DeleteSync_DeleteSource()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var sourceRepository = new SourceRepository(dbContext);
        var testSource = GetSourceSample();

        // Act
        var result = await sourceRepository.AddAsync(testSource, new CancellationToken());

        await sourceRepository.DeleteAsync(result, new CancellationToken());

        // Assert
        var deletedSource = await sourceRepository.GetAsync(result, new CancellationToken());
        deletedSource.Should().BeNull();
    }
}

