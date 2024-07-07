using WebApiForHikka.Domain.Models.ManyToMany;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.Relation;

public class DubAnimeRelationRepositoryTest : SharedRelationRepositoryTest<
    DubAnime, Dub, Anime,
    DubAnimeRelationRepository, DubRepository, AnimeRepository
>
{
    protected override async Task<(Guid firstId, Guid secondId)> CreateFirstAndSecondModels(
        (DubRepository firstRepository, AnimeRepository secondRepository) repostiroeis)
    {
        var firstId = await repostiroeis.firstRepository.AddAsync(GetDubModels.GetSample(), CancellationToken);

        var secondId =
            await repostiroeis.secondRepository.AddAsync(GetAnimeModels.GetSampleWithoutManyToMany(),
                CancellationToken);

        return (firstId, secondId);
    }

    protected override (DubRepository firstRepository, AnimeRepository secondRepository) GetFirstAndSecondRepositories(
        HikkaDbContext hikkaDbContext)
    {
        return (
            new DubRepository(hikkaDbContext),
            new AnimeRepository(hikkaDbContext)
        );
    }

    protected override Dub GetFirstModelSample()
    {
        return GetDubModels.GetSample();
    }

    protected override Anime GetSecondModelSample()
    {
        return GetAnimeModels.GetSample();
    }

    protected override DubAnime GetRelationModel(Guid firstId, Guid secondId)
    {
        return new DubAnime
        {
            FirstId = firstId,
            SecondId = secondId
        };
    }

    protected override DubAnimeRelationRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new DubAnimeRelationRepository(hikkaDbContext);
    }
}