using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.WebApi.Shared.RelationController;

namespace WebApiForHikka.Test.Controllers.Shared;

public abstract class RelationCrudControllerTest<
    TRelationModel, TFirstModel, TSecondModel,
    TRelationService, TFirstService, TSecondService,
    TRelationRepository, TFirstRepository, TSecondRepository,
    TController>
    : BaseControllerTest
    where TRelationModel : RelationModel<TFirstModel, TSecondModel>
    where TFirstModel :  class,  IModel
    where TSecondModel :  class,  IModel
    where TRelationService : IRelationCrudService<TRelationModel, TFirstModel, TSecondModel>
    where TFirstService :  ICrudService<TFirstModel>
    where TSecondService :  ICrudService<TSecondModel>
    where TRelationRepository : IRelationCrudRepository<TRelationModel, TFirstModel, TSecondModel>
    where TFirstRepository : ICrudRepository<TFirstModel>
    where TSecondRepository : ICrudRepository<TSecondModel>
    where TController : RelationCrudController<TRelationModel, TFirstModel, TSecondModel, TRelationService>
{

    protected abstract Task<TController> GetController(
        IServiceProvider alternativeServices);

    protected abstract TFirstModel GetFirstModelSample();
    protected abstract TSecondModel GetSecondModelSample();
    protected abstract void GetAllServices(IServiceCollection alternativeServices);


    [Fact]
    public virtual async Task CrudRelationController_Create_ReturnsOkObjectResult() 
    {
        //Arrange
        var serviceCollection = new ServiceCollection();
        GetAllServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var controller = await GetController(serviceProvider);

        var firstId = await serviceProvider.GetRequiredService<TFirstService>().CreateAsync(GetFirstModelSample(), CancellationToken);
        var secondId = await serviceProvider.GetRequiredService<TSecondService>().CreateAsync(GetSecondModelSample(), CancellationToken);


        //Act

        var result = await controller.Create(firstId, secondId, CancellationToken) as OkObjectResult;

        //Assert

        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();

    }





    [Fact]
    public virtual async Task CrudRelationController_Delete_ReturnsOkObjectResult() 
    {
        //Arrange
        var serviceCollection = new ServiceCollection();
        GetAllServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var controller = await GetController(serviceProvider);

        var firstId = await serviceProvider.GetRequiredService<TFirstService>().CreateAsync(GetFirstModelSample(), CancellationToken);
        var secondId = await serviceProvider.GetRequiredService<TSecondService>().CreateAsync(GetSecondModelSample(), CancellationToken);


        //Act

        await controller.Delete(firstId, secondId, CancellationToken);

        var result =  await controller.Check(firstId, secondId, CancellationToken) as NotFoundResult;

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NotFoundResult>();

    }

    [Fact]
    public virtual async Task CrudRelationController_Check_ReturnsOkObjectResult() 
    {
        //Arrange
        var serviceCollection = new ServiceCollection();
        GetAllServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var controller = await GetController(serviceProvider);

        var firstId = await serviceProvider.GetRequiredService<TFirstService>().CreateAsync(GetFirstModelSample(), CancellationToken);
        var secondId = await serviceProvider.GetRequiredService<TSecondService>().CreateAsync(GetSecondModelSample(), CancellationToken);


        //Act

        await controller.Create(firstId, secondId, CancellationToken);

        var result =  await controller.Check(firstId, secondId, CancellationToken) as OkResult;

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkResult>();

    }

    [Fact]
    public virtual async Task CrudRelationController_Check_ReturnsNotFoundObjectResult() 
    {
        //Arrange
        var serviceCollection = new ServiceCollection();
        GetAllServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var controller = await GetController(serviceProvider);

        //Act

        var result =  await controller.Check(Guid.NewGuid(), Guid.NewGuid(), CancellationToken) as NotFoundResult;

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NotFoundResult>();
    }





}
