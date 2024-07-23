﻿using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithSeoAddition.Kinds;

public class KindRepositoryTest : SharedRepositoryTestWithSeoAddition<
    Kind,
    KindRepository
>
{
    protected override KindRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new KindRepository(hikkaDbContext);
    }

    protected override Kind GetSample()
    {
        return GetKindModels.GetSample();
    }

    protected override Kind GetSampleForUpdate()
    {
        return GetKindModels.GetSampleForUpdate();
    }
}