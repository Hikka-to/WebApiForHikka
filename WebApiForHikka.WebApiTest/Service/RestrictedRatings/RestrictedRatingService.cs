using FluentAssertions;
using WebApiForHikka.Application.RestrictedRatings;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared;

namespace WebApiForHikka.Test.Service.RestrictedRatings;

public class RestrictedRatingServiceTest : SharedTest
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
    public async Task RestrictedRatingService_CreateAsync_ReturnsRestrictedRating()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var restrictedRatingRepository = new RestrictedRatingRepository(dbContext);
        var restrictedRatingService = new RestrictedRatingService(restrictedRatingRepository);

        var restrictedRatingSample = GetRestrictedRatingSample();

        // Act
        var id = await restrictedRatingService.CreateAsync(restrictedRatingSample, new CancellationToken());

        var result = await restrictedRatingService.GetAsync(id, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(restrictedRatingSample);
    }

    [Fact]
    public async Task RestrictedRatingService_DeleteAsync_DeletesRestrictedRating()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var restrictedRatingRepository = new RestrictedRatingRepository(dbContext);
        var restrictedRatingService = new RestrictedRatingService(restrictedRatingRepository);

        var restrictedRatingSample = GetRestrictedRatingSample();
        var id = await restrictedRatingService.CreateAsync(restrictedRatingSample, new CancellationToken());

        // Act
        await restrictedRatingService.DeleteAsync(id, new CancellationToken());
        var result = await restrictedRatingService.GetAsync(id, new CancellationToken());

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task RestrictedRatingService_UpdateAsync_UpdatesRestrictedRating()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var restrictedRatingRepository = new RestrictedRatingRepository(dbContext);
        var restrictedRatingService = new RestrictedRatingService(restrictedRatingRepository);
        var restrictedRatingSample = GetRestrictedRatingSample();
        var id = await restrictedRatingService.CreateAsync(restrictedRatingSample, new CancellationToken());
        restrictedRatingSample.Id = id;
        var updatedRestrictedRatingSample = new RestrictedRating
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
                Title = "test1",
            }
        };

        // Act
        await restrictedRatingService.UpdateAsync(updatedRestrictedRatingSample, new CancellationToken());

        var result = await restrictedRatingService.GetAsync(id, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(updatedRestrictedRatingSample);
    }
}