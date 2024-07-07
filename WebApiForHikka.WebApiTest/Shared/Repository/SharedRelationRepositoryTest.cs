using FluentAssertions;
using System.Diagnostics;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;

namespace WebApiForHikka.Test.Shared.Repository;

public abstract class SharedRelationRepositoryTest<
    TRelationModel, TFirstModel, TSecondModel,
    TRelationRepository, TFirstRepository, TSecondRepository
    > : SharedRepositoryTest<TRelationModel, TRelationRepository>
    where TRelationModel : RelationModel<TFirstModel, TSecondModel>
    where TRelationRepository : CrudRelationRepository<TRelationModel, TFirstModel, TSecondModel>, IRelationCrudRepository<TRelationModel, TFirstModel, TSecondModel>
    where TFirstRepository : class, ICrudRepository<TFirstModel>
    where TSecondRepository : class, ICrudRepository<TSecondModel>
    where TFirstModel : Model
    where TSecondModel : Model
{



    protected abstract (TFirstRepository firstRepository, TSecondRepository secondRepository) GetFirstAndSecondRepositories(HikkaDbContext hikkaDbContext);
    protected abstract Task<(Guid firstId, Guid secondId)> CreateFirstAndSecondModels((TFirstRepository firstRepository, TSecondRepository secondRepository) repostiroeis);

    protected abstract TFirstModel GetFirstModelSample();
    protected abstract TSecondModel GetSecondModelSample();

    protected abstract TRelationModel GetRelationModel(Guid firstId, Guid secondId);
    protected override TRelationModel GetSample()
    {
        throw new NotImplementedException("Don't use method GetSample in RelationRepositories tests");
    }

    protected override TRelationModel GetSampleForUpdate()
    {
        throw new NotImplementedException("Don't use method GetSampleForUpdate in RelationRepositories tests");
    }



    [Fact]
    public virtual async Task RepositoryRelation_DeleteByTwoIdsAsync_DeleteModel()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var firstAndSecondRepositories = GetFirstAndSecondRepositories(dbContext);
        TRelationRepository repository = GetRepository(dbContext);
        (Guid firstId, Guid secondId) sampleIds = await CreateFirstAndSecondModels(firstAndSecondRepositories);

        var relationModelId = await repository.AddAsync(GetRelationModel(sampleIds.firstId, sampleIds.secondId), CancellationToken);


        // Act

        var addedEntety = await repository.GetAsync(relationModelId, CancellationToken);


        await repository.DeleteAsync(sampleIds.firstId, sampleIds.secondId, CancellationToken);


        var result = await repository.GetAsync(relationModelId, CancellationToken);


        // Assert
        addedEntety.Should().NotBeNull();
        result.Should().BeNull();
    }

    [Fact]
    public virtual async Task RepositoryRelation_GetAsyncByTwoIds_ReturnsModel()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var firstAndSecondRepositories = GetFirstAndSecondRepositories(dbContext);
        TRelationRepository repository = GetRepository(dbContext);
        (Guid firstId, Guid secondId) sampleIds = await CreateFirstAndSecondModels(firstAndSecondRepositories);

        var RelationModelId = await repository.AddAsync(GetRelationModel(sampleIds.firstId, sampleIds.secondId), CancellationToken);


        // Act

        var addedEntety = await repository.GetAsync(sampleIds.firstId, sampleIds.secondId, CancellationToken);


        // Assert
        addedEntety.Should().NotBeNull();


    }

    [Fact]
    public virtual async Task RepositoryRelation_GetByTwoIds_ReturnsModel()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var firstAndSecondRepositories = GetFirstAndSecondRepositories(dbContext);
        TRelationRepository repository = GetRepository(dbContext);
        (Guid firstId, Guid secondId) sampleIds = await CreateFirstAndSecondModels(firstAndSecondRepositories);

        var RelationModelId = await repository.AddAsync(GetRelationModel(sampleIds.firstId, sampleIds.secondId), CancellationToken);


        // Act

        var addedEntety = repository.Get(sampleIds.firstId, sampleIds.secondId);


        // Assert
        addedEntety.Should().NotBeNull();
        Assert.True(addedEntety.FirstId == sampleIds.firstId);
        Assert.True(addedEntety.SecondId == sampleIds.secondId);


    }



    [Fact]
    public override async Task Repository_AddAsync_ReturnsModelAndId()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var firstAndSecondRepositories = GetFirstAndSecondRepositories(dbContext);
        TRelationRepository repository = GetRepository(dbContext);
        (Guid firstId, Guid secondId) sampleIds = await CreateFirstAndSecondModels(firstAndSecondRepositories);

        // Act

        var result = await repository.AddAsync(GetRelationModel(sampleIds.firstId, sampleIds.secondId), CancellationToken);

        // Assert
        result.Should().NotBeEmpty();
        var addedStatus = await repository.GetAsync(result, CancellationToken);
        addedStatus.Should().NotBeNull();
    }

    [Fact]
    public override async Task Repository_Deletesync_DeleteModel()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var firstAndSecondRepositories = GetFirstAndSecondRepositories(dbContext);
        TRelationRepository repository = GetRepository(dbContext);
        (Guid firstId, Guid secondId) sampleIds = await CreateFirstAndSecondModels(firstAndSecondRepositories);
        var result = await repository.AddAsync(GetRelationModel(sampleIds.firstId, sampleIds.secondId), CancellationToken);

        // Act

        await repository.DeleteAsync(result, CancellationToken);

        // Assert
        var deletedModel = await repository.GetAsync(result, CancellationToken);
        deletedModel.Should().BeNull();
    }

    [Fact]
    public async override Task Repository_GetAllAsync_ReturnsPage()
    {
        // Arrange

        var dbContext = GetDatabaseContext();
        var firstAndSecondRepositories = GetFirstAndSecondRepositories(dbContext);
        TRelationRepository repository = GetRepository(dbContext);

        List<(Guid firstId, Guid secondId)> sampleIds = new();

        List<Guid> relationModelsIds = new();


        for (int i = 0; i < 2; i++)
        {
            (Guid firstId, Guid secondId) sample = await CreateFirstAndSecondModels(firstAndSecondRepositories);
            sampleIds.Add(sample);
        }

        for (int i = 0; i < 2; i++)
        {

            relationModelsIds.Add(await repository.AddAsync(GetRelationModel(sampleIds[i].firstId, sampleIds[i].secondId), CancellationToken));
        }


        var dto = new FilterPagination { PageNumber = 1, PageSize = 1 };

        // Act
        var result = await repository.GetAllAsync(dto, CancellationToken);

        // Assert
        Assert.Single(result.Models);
    }

    [Fact]
    public override async Task Repository_GetAllAsync_ReturnsAllModels()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var firstAndSecondRepositories = GetFirstAndSecondRepositories(dbContext);
        TRelationRepository repository = GetRepository(dbContext);

        List<(Guid firstId, Guid secondId)> sampleIds = new();

        List<Guid> relationModelsIds = new();


        for (int i = 0; i < 2; i++)
        {
            (Guid firstId, Guid secondId) sample = await CreateFirstAndSecondModels(firstAndSecondRepositories);
            sampleIds.Add(sample);
        }

        for (int i = 0; i < 2; i++)
        {

            relationModelsIds.Add(await repository.AddAsync(GetRelationModel(sampleIds[i].firstId, sampleIds[i].secondId), CancellationToken));
        }

        // Act
        var result = await repository.GetAllAsync(CancellationToken);

        // Assert
        Assert.Equal(relationModelsIds.Count, result.Count);
    }



    [Fact]
    public override async Task Repository_GetAllModelsByIdsAsync_ReturnsModelsByIds()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var firstAndSecondRepositories = GetFirstAndSecondRepositories(dbContext);
        TRelationRepository repository = GetRepository(dbContext);

        List<(Guid firstId, Guid secondId)> sampleIds = new();

        List<Guid> relationModelsIds = new();


        for (int i = 0; i < 2; i++)
        {
            (Guid firstId, Guid secondId) sample = await CreateFirstAndSecondModels(firstAndSecondRepositories);
            sampleIds.Add(sample);
        }

        for (int i = 0; i < 2; i++)
        {

            relationModelsIds.Add(await repository.AddAsync(GetRelationModel(sampleIds[i].firstId, sampleIds[i].secondId), CancellationToken));
        }



        // Act
        var result = await repository.GetAllModelsByIdsAsync(relationModelsIds, CancellationToken);

        // Assert
        Assert.Equal(relationModelsIds.Count, result.Count);
    }

    [Fact]
    public async override Task Repository_GetAsync_ReturnsModel()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var firstAndSecondRepositories = GetFirstAndSecondRepositories(dbContext);
        TRelationRepository repository = GetRepository(dbContext);
        (Guid firstId, Guid secondId) sampleIds = await CreateFirstAndSecondModels(firstAndSecondRepositories);

        var RelationModelId = await repository.AddAsync(GetRelationModel(sampleIds.firstId, sampleIds.secondId), CancellationToken);


        // Act

        var addedEntety = await repository.GetAsync(RelationModelId, CancellationToken);


        // Assert
        addedEntety.Should().NotBeNull();
    }


    [Fact]
    public async override Task Repository_Get_ReturnsModel()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var firstAndSecondRepositories = GetFirstAndSecondRepositories(dbContext);
        TRelationRepository repository = GetRepository(dbContext);
        (Guid firstId, Guid secondId) sampleIds = await CreateFirstAndSecondModels(firstAndSecondRepositories);

        // Act

        var id = await repository.AddAsync(GetRelationModel(sampleIds.firstId, sampleIds.secondId), CancellationToken);


        var rersult = repository.Get(id);



        // Assert
        rersult.Should().NotBeNull();
    }

    [Fact]
    public override async Task Repository_UpdateAsync_UpdateModel()
    {
         // Arrange
        var dbContext = GetDatabaseContext();
        var firstAndSecondRepositories = GetFirstAndSecondRepositories(dbContext);
        TRelationRepository repository = GetRepository(dbContext);
        (Guid firstId, Guid secondId) sampleIds = await CreateFirstAndSecondModels(firstAndSecondRepositories);
        (Guid firstId, Guid secondId) secondSampleIds = await CreateFirstAndSecondModels(firstAndSecondRepositories);

        var RelationModelId = await repository.AddAsync(GetRelationModel(sampleIds.firstId, sampleIds.secondId), CancellationToken);
        var newModelForUpdate = GetRelationModel(secondSampleIds.firstId, secondSampleIds.secondId);

        newModelForUpdate.Id = RelationModelId;


        // Act
        await repository.UpdateAsync(newModelForUpdate, CancellationToken);

        var result = await repository.GetAsync(newModelForUpdate.Id, CancellationToken);

        // Assert
        result.Should().NotBeNull();
        Assert.True(result.FirstId == newModelForUpdate.FirstId);
        Assert.True(result.SecondId == newModelForUpdate.SecondId);
    }


}
