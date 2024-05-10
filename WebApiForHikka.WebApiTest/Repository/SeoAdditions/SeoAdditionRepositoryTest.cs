using FluentAssertions;
using WebApiForHikka.Constants.Users;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared;

namespace WebApiForHikka.Test.Repository.SeoAdditions;
public class SeoAdditionRepositoryTest : SharedTest
{
    private SeoAddition GetSeoAdditionSample() 
    {
        return new SeoAddition {
            Description = "test",
            Slug = "test",
            Title = "test",
            Image = "test",
            ImageAlt = "test",
            SocialImage = "test",
            SocialImageAlt = "test",
            SocialTitle = "test",
            SocialType = "test",
        };
    }


    [Fact]
    public async Task SeoAdditionRepository_AddSync_ReturnsSeoAdditionAndId() 
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var SeoAdditionRepository = new SeoAdditionRepository(dbContext);
        var testSeoAddition = GetSeoAdditionSample(); 

        // Act
        var result = await SeoAdditionRepository.AddAsync(testSeoAddition, new CancellationToken());

        // Assert
        result.Should().NotBeEmpty();
        var addedSeoAddition = await SeoAdditionRepository.GetAsync(result, new CancellationToken());
        addedSeoAddition.Should().NotBeNull();
        addedSeoAddition.Should().BeEquivalentTo(testSeoAddition);

    }

    [Fact]
    public async Task SeoAdditionRepository_UpdateAsync_UpdatesSeoAddition()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var testSeoAddition = GetSeoAdditionSample();
        var id = await seoAdditionRepository.AddAsync(testSeoAddition, new CancellationToken());
        testSeoAddition.Id = id;

        // Prepare the updated SEO addition
        var updatedSeoAddition = new SeoAddition
        {
            Id = id,
            Image = "test1",
            ImageAlt = "test1",
            Slug = "test1",
            Description = "test1",
            SocialImage = "test1",
            SocialTitle = "test1",
            Title = "test1",
            SocialType = "test1",
            SocialImageAlt = "test1"
        };

        // Act
        await seoAdditionRepository.UpdateAsync(updatedSeoAddition, new CancellationToken());

        var result = await seoAdditionRepository.GetAsync(id, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(updatedSeoAddition);
    }


    [Fact]
    public async Task SeoAdditionRepository_DeleteSync_DeleteSeoAddition() 
    {
        // Arrage
        var dbContext = await GetDatabaseContext();
        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var testSeoAddition = GetSeoAdditionSample();

        // Act
        var result = await seoAdditionRepository.AddAsync(testSeoAddition, new CancellationToken());

        await seoAdditionRepository.DeleteAsync(result, new CancellationToken());

        // Assert
        var testResult = await seoAdditionRepository.GetAsync(result, new CancellationToken());
        testResult.Should().BeNull();
    }
}
