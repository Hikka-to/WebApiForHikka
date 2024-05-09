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
    public async Task SeoAdditionRepository_UpdateSync_ReturnsUpdatedSeoAddition()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var SeoAdditionRepository = new SeoAdditionRepository(dbContext);
        var testSeoAddition = GetSeoAdditionSample(); 

        // Act
        var result = await SeoAdditionRepository.AddAsync(testSeoAddition, new CancellationToken());
        testSeoAddition.Id = result;
        testSeoAddition.Image = "test1";
        testSeoAddition.ImageAlt = "test1";
        testSeoAddition.Slug = "test1";
        testSeoAddition.Description = "test1";
        testSeoAddition.SocialImage = "test1";
        testSeoAddition.SocialTitle = "test1";
        testSeoAddition.Title = "test1";
        testSeoAddition.SocialType = "test1";
        testSeoAddition.SocialImageAlt = "test1";

        await SeoAdditionRepository.UpdateAsync(testSeoAddition, new CancellationToken());

        var newObject = await SeoAdditionRepository.GetAsync(testSeoAddition.Id, new CancellationToken());

        // Assert
        newObject.Should().NotBeNull();
        newObject.Should().BeEquivalentTo(testSeoAddition);

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
