﻿using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithSeoAddition.Tags;

public class TagRepositoryTest : SharedRepositoryTestWithSeoAddition<
    Tag,
    TagRepository
    >
{
    protected override TagRepository GetRepository(HikkaDbContext hikkaDbContext) =>
        new(hikkaDbContext);

    protected override Tag GetSample() => GetTagModels.GetSample();
    protected override Tag GetSampleForUpdate() => GetTagModels.GetSampleForUpdate();
}