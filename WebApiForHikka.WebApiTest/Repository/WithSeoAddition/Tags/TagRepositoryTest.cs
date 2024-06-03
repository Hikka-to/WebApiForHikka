
using FluentAssertions;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithSeoAddition.Tags;

public class TagRepositoryTest : SharedRepositoryTestWithSeoAddition<
    Tag,
    TagRepository
    >
{
    protected override TagRepository GetRepository(HikkaDbContext hikkaDbContext) =>
        new(hikkaDbContext);

    protected override Tag GetSample() => new()
    {
        Alises = ["test"],
        EngName = "test",
        IsGenre = true,
        Name = "test",
        SeoAddition = GetSeoAdditionSample(),
    };

    protected override Tag GetSampleForUpdate() => new()
    {
        Alises = ["test1"],
        EngName = "test1",
        IsGenre = false,
        Name = "test1",
        SeoAddition = GetSeoAdditionSampleUpdate(),
        ParentTag = new()
        {
            Alises = ["test"],
            EngName = "test",
            IsGenre = true,
            Name = "test",
            SeoAddition = GetSeoAdditionSample(),
        },
    };

    [Fact]
    public override async Task Repository_UpdateAsync_UpdateModel()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var repository = GetRepository(dbContext);
        var sample = GetSample();
        var id = await repository.AddAsync(sample, CancellationToken);
        var createModel = await repository.GetAsync(id, CancellationToken);
        sample.Id = createModel.Id;
        sample.SeoAddition.Id = createModel.SeoAddition.Id;
        var updatedSample = GetSampleForUpdate();
        updatedSample.Id = createModel.Id;
        updatedSample.SeoAddition.Id = createModel.SeoAddition.Id;

        // Act
        await repository.UpdateAsync(updatedSample, CancellationToken);

        var result = await repository.GetAsync(id, CancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(updatedSample);
    }
}