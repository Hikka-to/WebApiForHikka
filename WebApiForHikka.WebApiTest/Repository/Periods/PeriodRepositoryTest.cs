using FluentAssertions;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.ModelsWithSeoAddition;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared;

namespace WebApiForHikka.Test.Repository.Periods;


public class PeriodRepositoryTest : SharedTest
{
    public Period GetPeriodSample()
    {
        return new Period()
        {
            Name = "Test",
            SeoAddition = new SeoAddition()
            {
                Description = "test",
                Slug = "test",
                Title = "Test",
            },
        };
    }

    [Fact]
    public async Task PeriodRepository_AddSync_ReturnsPeriodAndId()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var periodRepository = new PeriodRepository(dbContext);
        var testPeriod = GetPeriodSample();

        // Act
        var result = await periodRepository.AddAsync(testPeriod, new CancellationToken());

        // Assert
        result.Should().NotBeEmpty();
        var addedPeriod = await periodRepository.GetAsync(result, new CancellationToken());
        addedPeriod.Should().NotBeNull();
        addedPeriod.Should().BeEquivalentTo(testPeriod);
    }

    [Fact]
    public async Task PeriodRepository_UpdateAsync_UpdatesPeriod()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var periodRepository = new PeriodRepository(dbContext);
        var testPeriod = GetPeriodSample();
        var id = await periodRepository.AddAsync(testPeriod, new CancellationToken());
        testPeriod.Id = id;

        // Prepare the updated period
        var updatedPeriod = new Period
        {
            Id = id,
            Name = "Test1",
            SeoAddition = new SeoAddition
            {
                Description = "Test1",
                Slug = "Test1",
                Title = "Test1"
            }
        };

        // Act
        await periodRepository.UpdateAsync(updatedPeriod, new CancellationToken());

        var result = await periodRepository.GetAsync(id, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(updatedPeriod);
    }


    [Fact]
    public async Task PeriodRepository_DeleteSync_DeletePeriod()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var periodRepository = new PeriodRepository(dbContext);
        var testPeriod = GetPeriodSample();

        // Act
        var result = await periodRepository.AddAsync(testPeriod, new CancellationToken());

        await periodRepository.DeleteAsync(result, new CancellationToken());

        // Assert
        var deletedPeriod = await periodRepository.GetAsync(result, new CancellationToken());
        deletedPeriod.Should().BeNull();
    }
}
