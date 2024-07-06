﻿using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Mediaplayers;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithoutSeoAddition.Mediaplayers;

public class MediaplayerRepositoryTest : SharedRepositoryTest<
    Mediaplayer,
    MediaplayerRepository
    >

{
    protected override MediaplayerRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new MediaplayerRepository(hikkaDbContext);
    }

    protected override Mediaplayer GetSample() => GetMediaplayerModels.GetSample();
    protected override Mediaplayer GetSampleForUpdate() => GetMediaplayerModels.GetSampleForUpdate();

}

