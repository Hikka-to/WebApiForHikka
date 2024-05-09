using FluentAssertions;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared;

namespace WebApiForHikka.Test.Service.SeoAdditions;

public class SeoAdditionServiceTest : SharedTest
{
    private SeoAddition GetSeoAdditionSample()
    {
        return new SeoAddition
        {
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
    public async Task SeoAdditionsService_CreateAsync_ReturnsSeoAddition() 
    {
        //Arrage
        var dbContext = await GetDatabaseContext();
        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var seoAdditionService = new SeoAdditionService(seoAdditionRepository);

        var seoAdditionSample = GetSeoAdditionSample();

        //Act
        var Id = await seoAdditionService.CreateAsync(seoAdditionSample, new CancellationToken());

        var result = await seoAdditionService.GetAsync(Id, new CancellationToken());

        //Assert

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(seoAdditionSample);
    }

    [Fact]
    public async Task SeoAdditionsService_DeleteAsync_DeletesSeoAddition()
    {
        //Arrage
        var dbContext = await GetDatabaseContext();
        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var seoAdditionService = new SeoAdditionService(seoAdditionRepository);

        var seoAdditionSample = GetSeoAdditionSample();
        var Id = await seoAdditionService.CreateAsync(seoAdditionSample, new CancellationToken());

        //Act

        await seoAdditionService.DeleteAsync(Id, new CancellationToken());
        var result = await seoAdditionService.GetAsync(Id, new CancellationToken());

        //Assert

        result.Should().BeNull();
    }

    [Fact]
    public async Task SeoAdditionsService_UpdateAsync_UpdatesSeoAddition()
    {
        //Arrage
        var dbContext = await GetDatabaseContext();
        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var seoAdditionService = new SeoAdditionService(seoAdditionRepository);
        var seoAdditionSample = GetSeoAdditionSample();
        var id = await seoAdditionService.CreateAsync(seoAdditionSample, new CancellationToken());
        seoAdditionSample.Id = id;
        var newAdditionSample = new SeoAddition
        {
            Id = id,
            Description = "test1",
            Slug = "test1",
            Title = "test1",
            Image = "test1",
            ImageAlt = "test1",
            SocialImage = "test1",
            SocialImageAlt = "test1",
            SocialTitle = "test1",
            SocialType = "test1",
        };

        //Act

        await seoAdditionService.UpdateAsync(newAdditionSample, new CancellationToken());

        var result = await seoAdditionService.GetAsync(id, new CancellationToken());

        //Assert

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(newAdditionSample);
    }
}
