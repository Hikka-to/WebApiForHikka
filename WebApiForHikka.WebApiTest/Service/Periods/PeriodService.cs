using FluentAssertions;
using WebApiForHikka.Application.Periods;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.ModelsWithSeoAddition;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared;

namespace WebApiForHikka.Test.Service.Periods;

public class PeriodServiceTest : SharedTest
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
                Title = "test",
            }
        };
    }

    [Fact]
    public async Task PeriodService_CreateAsync_ReturnsPeriod()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var periodRepository = new PeriodRepository(dbContext);
        var periodService = new PeriodService(periodRepository);

        var periodSample = GetPeriodSample();

        // Act
        var id = await periodService.CreateAsync(periodSample, new CancellationToken());

        var result = await periodService.GetAsync(id, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(periodSample);
    }

    [Fact]
    public async Task PeriodService_DeleteAsync_DeletesPeriod()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var periodRepository = new PeriodRepository(dbContext);
        var periodService = new PeriodService(periodRepository);

        var periodSample = GetPeriodSample();
        var id = await periodService.CreateAsync(periodSample, new CancellationToken());

        // Act
        await periodService.DeleteAsync(id, new CancellationToken());
        var result = await periodService.GetAsync(id, new CancellationToken());

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task PeriodService_UpdateAsync_UpdatesPeriod()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var periodRepository = new PeriodRepository(dbContext);
        var periodService = new PeriodService(periodRepository);
        var periodSample = GetPeriodSample();
        var id = await periodService.CreateAsync(periodSample, new CancellationToken());
        periodSample.Id = id;
        var updatedPeriodSample = new Period
        {
            Id = id,
            Name = "Test1",
            SeoAddition = new SeoAddition
            {
                Description = "test1",
                Slug = "test1",
                Title = "test1",
            }
        };

        // Act
        await periodService.UpdateAsync(updatedPeriodSample, new CancellationToken());

        var result = await periodService.GetAsync(id, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(updatedPeriodSample);
    }
}