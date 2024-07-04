//using WebApiForHikka.Application.Shared.Relation;
//using WebApiForHikka.Domain.Models;

//namespace WebApiForHikka.Test.Shared.Repository;

//public abstract class SharedRelationRepositoryTest<TModel, TRepository> : SharedRepositoryTest<TModel, TRepository> where TModel : RelationModel, TRepository where TRepository : IRelationCrudRepository<TModel>
//{

//    [Fact]
//    public virtual async Task RepositoryRelation_DeleteByTwoIdsAsync_DeleteModel()
//    {
//        // Arrange
//        var dbContext = GetDatabaseContext();
//        TRepository Repository = GetRepository(dbContext);
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
