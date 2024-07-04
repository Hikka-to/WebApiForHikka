namespace WebApiForHikka.Test.Shared.Repository;

//public abstract class SharedRelationRepositoryTest<
//    TModel, TFirstModel, TSecondModel, 
//    TRelationRepository, TFirstRepository, TSecondRepository> : SharedRepositoryTest<TModel, TRelationRepository>
//    where TModel : RelationModel<TFirstModel, TSecondModel>
//    where TRelationRepository : IRelationCrudRepository<TModel, TFirstModel, TSecondModel>
//    where TFirstRepository : ICrudRepository<TFirstModel>
//    where TSecondRepository : ICrudRepository<TSecondModel>
//    where TFirstModel : Model
//    where TSecondModel : Model
//{

//    [Fact]
//    public virtual async Task RepositoryRelation_DeleteByTwoIdsAsync_DeleteModel()
//    {
//        // Arrange
//        var dbContext = GetDatabaseContext();
//        TRelationRepository Repository = GetRepository(dbContext);
//        (Guid firstId, Guid secondId) sample = GetSample();

//        // Act
//        var result = await Repository.DeleteAsync(sample, CancellationToken);

//        // Assert
//        result.Should().NotBeEmpty();
//        var addedStatus = await Repository.GetAsync(result, CancellationToken);
//        addedStatus.Should().NotBeNull();
//        addedStatus.Should().BeEquivalentTo(sample);
//    }

//    [Fact]
//    public virtual async Task RepositoryRelation_GetAsync_ReturnsModel()
//    {

//    }

//    [Fact]
//    public virtual async Task RepositoryRelation_Get_ReturnModel()
//    {

//    }

//}
