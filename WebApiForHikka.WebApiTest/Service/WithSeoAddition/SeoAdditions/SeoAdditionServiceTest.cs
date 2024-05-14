﻿using FluentAssertions;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.SeoAdditions;

public class SeoAdditionServiceTest : SharedServiceTest<SeoAddition, SeoAdditionService>
{
    protected override SeoAddition GetSample()
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

    protected override SeoAddition GetSampleForUpdate()
    {
        return new SeoAddition
        {
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
    }

    protected override SeoAdditionService GetService(HikkaDbContext hikkaDbContext)
    {
        SeoAdditionRepository repository = new SeoAdditionRepository(hikkaDbContext);

        return new SeoAdditionService(repository);

    }
}
