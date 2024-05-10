using FluentAssertions;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared;

namespace WebApiForHikka.Test.Repository.Kinds;

public class KindRepositoryTest : SharedTest
{
    public Kind GetKindSample()
    {
        return new Kind()
        {
            Hint = "Test",
            Slug = "Test",
            SeoAddition = new SeoAddition()
            {
                Description = "Test",
                Slug = "Test",
                Title = "Test",

            },
        };
    }
    [Fact]
    public async Task KindRepository_AddSync_ReturnsKindAndId()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var kindRepository = new KindRepository(dbContext);
        var testKind = GetKindSample();

        // Act
        var result = await kindRepository.AddAsync(testKind, new CancellationToken());

        // Assert
        result.Should().NotBeEmpty();
        var addedKind = await kindRepository.GetAsync(result, new CancellationToken());
        addedKind.Should().NotBeNull();
        addedKind.Should().BeEquivalentTo(testKind);
    }

    [Fact]
    public async Task KindRepository_UpdateAsync_UpdatesKind()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var kindRepository = new KindRepository(dbContext);
        var testKind = GetKindSample();
        var id = await kindRepository.AddAsync(testKind, new CancellationToken());
        testKind.Id = id;

        // Prepare the updated kind
        var updatedKind = new Kind
        {
            Id = id,
            Hint = "Test1",
            Slug = "Test1",
            SeoAddition = new SeoAddition
            {
                Description = "Test1",
                Slug = "Test1",
                Title = "Test1"
            }
        };

        // Act
        await kindRepository.UpdateAsync(updatedKind, new CancellationToken());

        var result = await kindRepository.GetAsync(id, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(updatedKind);
    }

    [Fact]
    public async Task KindRepository_DeleteSync_DeleteKind()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var kindRepository = new KindRepository(dbContext);
        var testKind = GetKindSample();

        // Act
        var result = await kindRepository.AddAsync(testKind, new CancellationToken());

        await kindRepository.DeleteAsync(result, new CancellationToken());

        // Assert
        var deletedKind = await kindRepository.GetAsync(result, new CancellationToken());
        deletedKind.Should().BeNull();
    }
}
