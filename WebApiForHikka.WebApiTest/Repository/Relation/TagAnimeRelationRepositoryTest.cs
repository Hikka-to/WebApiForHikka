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

        var secondId = await repostiroeis.secondRepository.AddAsync(GetAnimeModels.GetSample(), CancellationToken);

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
            Id = firstId,
            SecondId = secondId,
        };
    }

    protected override TagAnimeRelationRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new TagAnimeRelationRepository(hikkaDbContext);
    }

    protected override TagAnime GetSample()
    {
        throw new NotImplementedException("Don't use this method in RelationRepositories tests");
    }

    protected override TagAnime GetSampleForUpdate()
    {
        throw new NotImplementedException();
    }

    public override Task Repository_AddAsync_ReturnsModelAndId()
    {
        return base.Repository_AddAsync_ReturnsModelAndId();
    }

    public override Task Repository_Deletesync_DeleteModel()
    {
        return base.Repository_Deletesync_DeleteModel();
    }

    public override Task Repository_GetAllAsync_ReturnsPage()
    {
        return base.Repository_GetAllAsync_ReturnsPage();
    }

    public override Task Repository_GetAsync_ReturnsModel()
    {
        return base.Repository_GetAsync_ReturnsModel();
    }

    public override Task Repository_UpdateAsync_UpdateModel()
    {
        return base.Repository_UpdateAsync_UpdateModel();
    }

    public override Task RepositoryRelation_DeleteByTwoIdsAsync_DeleteModel()
    {
        return base.RepositoryRelation_DeleteByTwoIdsAsync_DeleteModel();
    }

    public override Task RepositoryRelation_GetAsync_ReturnsModel()
    {
        return base.RepositoryRelation_GetAsync_ReturnsModel();
    }

    public override Task RepositoryRelation_Get_ReturnModel()
    {
        return base.RepositoryRelation_Get_ReturnModel();
    }
}
