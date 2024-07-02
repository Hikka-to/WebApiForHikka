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
}