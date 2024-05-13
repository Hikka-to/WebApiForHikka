using FluentAssertions;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.ModelsWithSeoAddition;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared;

namespace WebApiForHikka.Test.Repository.RestrictedRatings;

public class RestrictedRatingRepositoryTest : SharedTest
{
    public RestrictedRating GetRestrictedRatingSample()
    {
        return new RestrictedRating()
        {
            Hint = "test",
            Icon = "test",
            Name = "test",
            Value = 1,
            SeoAddition = new SeoAddition()
            {
                Description = "test",
                Slug = "test",
                Title = "test",
            }
        };
    }

    [Fact]
    public async Task RestrictedRatingRepository_AddSync_ReturnsRestrictedRatingAndId()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var restrictedRatingRepository = new RestrictedRatingRepository(dbContext);
        var testRestrictedRating = GetRestrictedRatingSample();

        // Act
        var result = await restrictedRatingRepository.AddAsync(testRestrictedRating, new CancellationToken());

        // Assert
        result.Should().NotBeEmpty();
        var addedRestrictedRating = await restrictedRatingRepository.GetAsync(result, new CancellationToken());
        addedRestrictedRating.Should().NotBeNull();
        addedRestrictedRating.Should().BeEquivalentTo(testRestrictedRating);
    }

    [Fact]
    public async Task RestrictedRatingRepository_UpdateAsync_UpdatesRestrictedRating()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var restrictedRatingRepository = new RestrictedRatingRepository(dbContext);
        var testRestrictedRating = GetRestrictedRatingSample();
        var id = await restrictedRatingRepository.AddAsync(testRestrictedRating, new CancellationToken());
        testRestrictedRating.Id = id;

        // Prepare the updated restricted rating
        var updatedRestrictedRating = new RestrictedRating
        {
            Id = id,
            Hint = "test1",
            Icon = "test1",
            Name = "test1",
            Value = 2,
            SeoAddition = new SeoAddition
            {
                Description = "test1",
                Slug = "test1",
                Title = "test1"
            }
        };

        // Act
        await restrictedRatingRepository.UpdateAsync(updatedRestrictedRating, new CancellationToken());

        var result = await restrictedRatingRepository.GetAsync(id, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(updatedRestrictedRating);
    }

    [Fact]
    public async Task RestrictedRatingRepository_DeleteSync_DeleteRestrictedRating()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var restrictedRatingRepository = new RestrictedRatingRepository(dbContext);
        var testRestrictedRating = GetRestrictedRatingSample();

        // Act
        var result = await restrictedRatingRepository.AddAsync(testRestrictedRating, new CancellationToken());

        await restrictedRatingRepository.DeleteAsync(result, new CancellationToken());

        // Assert
        var deletedRestrictedRating = await restrictedRatingRepository.GetAsync(result, new CancellationToken());
        deletedRestrictedRating.Should().BeNull();
    }
}
