using System.Text.RegularExpressions;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.Relation;

public class SeasonRepositoryTest : SharedRelationRepositoryTest<
    Season, Anime, AnimeGroup,
    SeasonRelationRepository, AnimeRepository, AnimeGroupRepository
>
{
    protected override async Task<(Guid firstId, Guid secondId)> CreateFirstAndSecondModels(
        (AnimeRepository firstRepository, AnimeGroupRepository secondRepository) repostiroeis)
    {
        var firstId = await repostiroeis.firstRepository.AddAsync(GetAnimeModels.GetSampleWithoutManyToMany(), CancellationToken);

        var secondId =
            await repostiroeis.secondRepository.AddAsync(GetAnimeGroupModels.GetSample(),
                CancellationToken);

        return (firstId, secondId);
    }

    protected override (AnimeRepository firstRepository, AnimeGroupRepository secondRepository)
        GetFirstAndSecondRepositories(HikkaDbContext hikkaDbContext)
    {
        return (
            new AnimeRepository(hikkaDbContext),
            new AnimeGroupRepository(hikkaDbContext)
        );
    }

    protected override Anime GetFirstModelSample()
    {
        return GetAnimeModels.GetSampleWithoutManyToMany();
    }

    protected override AnimeGroup GetSecondModelSample()
    {
        return GetAnimeGroupModels.GetSample();
    }

    protected override Season GetRelationModel(Guid firstId, Guid secondId)
    {
        return new Season
        {
            FirstId = firstId,
            SecondId = secondId,
            Name = "Test"
        };
    }

    protected override SeasonRelationRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new SeasonRelationRepository(hikkaDbContext);
    }
}