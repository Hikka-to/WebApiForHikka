using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using WebApiForHikka.Domain.Models.ManyToMany;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Repository;
using Xunit.Sdk;

namespace WebApiForHikka.Test.Repository.Relation;

public class TagAnimeRelationRepositoryTest : SharedRelationRepositoryTest<
    TagAnime, Tag, Anime,
    TagAnimeRelationRepository, TagRepository, AnimeRepository
    >
{
    protected override async Task<(Guid firstId, Guid secondId)> CreateFirstAndSecondModels((TagRepository firstRepository, AnimeRepository secondRepository) repostiroeis)
    {
        var firstId = await repostiroeis.firstRepository.AddAsync(GetTagModels.GetSample(), CancellationToken);

        var secondId = await repostiroeis.secondRepository.AddAsync(GetAnimeModels.GetSampleWithoutManyToMany(), CancellationToken);

        return (firstId, secondId);
    }

    protected override (TagRepository firstRepository, AnimeRepository secondRepository) GetFirstAndSecondRepositories(HikkaDbContext hikkaDbContext)
    {
        return (
            new TagRepository(hikkaDbContext),
            new AnimeRepository(hikkaDbContext)
            );

    }

    protected override Tag GetFirstModelSample()
    {
        return GetTagModels.GetSample();
    }

    protected override Anime GetSecondModelSample()
    {
        return GetAnimeModels.GetSample();
    }

    protected override TagAnime GetRelationModel(Guid firstId, Guid secondId)
    {
        return new TagAnime()
        {
            FirstId = firstId,
            SecondId = secondId,
        };
    }

    protected override TagAnimeRelationRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new TagAnimeRelationRepository(hikkaDbContext);
    }
}
