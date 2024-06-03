using FluentAssertions;
using WebApiForHikka.Application.WithSeoAddition.Tags;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.Tags;

public class TagServiceTest : SharedServiceTestWithSeoAddition<
    Tag,
    TagService
    >
{
    protected override TagService GetService(HikkaDbContext hikkaDbContext)
    {
        TagRepository rep = new(hikkaDbContext);

        return new TagService(rep);
    }

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
        ParentTag = GetSample(),
    };

    [Fact]
    public override async Task Service_UpdateAsync_UpdateModel()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var service = GetService(dbContext);
        var sample = GetSample();
        var id = await service.CreateAsync(sample, CancellationToken);
        var createModel = await service.GetAsync(id, CancellationToken);
        sample.Id = createModel.Id;
        sample.SeoAddition.Id = createModel.SeoAddition.Id;
        var updatedSample = GetSampleForUpdate();
        updatedSample.Id = createModel.Id;
        updatedSample.SeoAddition.Id = createModel.SeoAddition.Id;

        // Act
        await service.UpdateAsync(updatedSample, CancellationToken);

        var result = await service.GetAsync(id, CancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(updatedSample);
    }
}