﻿using WebApiForHikka.Application.Formats;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.Formats;

public class FormatServiceTest : SharedServiceTestWithSeoAddition<Format, FormatService>
{
    protected override Format GetSample() => new()
    {
        Name = "Test",
        SeoAddition = GetSeoAdditionSample(),
    };

    protected override Format GetSampleForUpdate() => new()
    {
        Name = "Test1",
        SeoAddition = GetSeoAdditionSampleUpdate(),
    };

    protected override FormatService GetService(HikkaDbContext hikkaDbContext)
    {
        FormatRepository repository = new(hikkaDbContext);

        return new FormatService(repository);
    }
}