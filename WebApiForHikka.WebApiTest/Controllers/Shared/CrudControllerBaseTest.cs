using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.Test.Controller.Shared;

public abstract class CrudControllerBaseTest
    <TController, TCrudService,
    TModel, TIRepository,
    TUpdateDto, TCreateDto, TGetDto, TReturnPageDto>
    : BaseControllerTest
    where TController : ICrudController<TUpdateDto, TCreateDto>
    where TCrudService : CrudService<TModel, TIRepository>
    where TModel : class, IModel
    where TIRepository : ICrudRepository<TModel>
    where TUpdateDto : ModelDto
    where TReturnPageDto : ReturnPageDto<TGetDto>

{
    protected abstract Task<TController> GetController(AllServicesInController allServicesInController, IServiceProvider alternativeServices);
    protected abstract TCreateDto GetCreateDtoSample();
    protected abstract TUpdateDto GetUpdateDtoSample();
    protected abstract TGetDto GetGetDtoSample();
    protected abstract TModel GetModelSample();
    protected abstract AllServicesInController GetAllServices(IServiceCollection alternativeServices);

    protected virtual void MutationBeforeDtoCreation(TCreateDto createDto, AllServicesInController allServicesInController, IServiceProvider alternativeServices) { }
    protected virtual void MutationBeforeDtoUpdate(TUpdateDto updateDto, AllServicesInController allServicesInController, IServiceProvider alternativeServices) { }

    protected virtual ICollection<TModel> GetCollectionOfModels(int howMany)
    {
        ICollection<TModel> seoAdditions = new List<TModel>();
        for (int i = 0; i < howMany; ++i)
        {
            seoAdditions.Add(GetModelSample());
        }
        return seoAdditions;

    }


    [Fact]
    public virtual async Task CrudController_Get_ReturnsNotFound()
    {
        //Arrange
        var serviceCollection = new ServiceCollection();
        var services = GetAllServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        TController controller = await GetController(services, serviceProvider);

        //Act

        var result = await controller.Get(new Guid(), CancellationToken);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NotFoundResult>();

    }


    [Fact]
    public virtual async Task CrudController_GetAll_ReturnsReturnPageDto()
    {
        //Arrange
        var serviceCollection = new ServiceCollection();
        var services = GetAllServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        TController controller = await GetController(services, serviceProvider);
        foreach (var item in GetCollectionOfModels(10))
        {
            MutationBeforeDtoCreation(GetCreateDtoSample(), services, serviceProvider);
            await services.CrudService.CreateAsync(item, CancellationToken);
        }

        //Act

        var result = await controller.GetAll(FilterPaginationDto, CancellationToken) as OkObjectResult;


        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();

        var returnPageDto = result.Value as TReturnPageDto;

        returnPageDto.Should().BeOfType<TReturnPageDto>();

    }


    [Fact]
    public virtual async Task CrudController_Create_ReturnsCreateResponseDto()
    {
        //Arrange
        var serviceCollection = new ServiceCollection();
        var services = GetAllServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        TController controller = await GetController(services, serviceProvider);

        //Act
        var dtoSample = GetCreateDtoSample();
        MutationBeforeDtoCreation(dtoSample, services, serviceProvider);
        var result = await controller.Create(dtoSample, CancellationToken) as OkObjectResult;

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
        var createResponseDto = result.Value as CreateResponseDto;
        createResponseDto.Should().BeOfType<CreateResponseDto>();
    }


    [Fact]
    public virtual async Task CrudController_Delete_ReturnsNoContent()
    {
        //Arrange
        var serviceCollection = new ServiceCollection();
        var services = GetAllServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var id = await services.CrudService.CreateAsync(GetModelSample(), CancellationToken);
        TController controller = await GetController(services, serviceProvider);

        //Act
        var result = await controller.Delete(id, CancellationToken);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();

    }


    [Fact]
    public virtual async Task CrudController_Get_ReturnsOkObjectResult()
    {
        //Arrange
        var serviceCollection = new ServiceCollection();
        var services = GetAllServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        TController controller = await GetController(services, serviceProvider);

        var model = GetModelSample();
        var id = await services.CrudService.CreateAsync(model, CancellationToken);

        //Act

        var result = await controller.Get(id, CancellationToken);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }


    [Fact]
    public virtual async Task CrudController_Put_ReturnsNoContent()
    {
        //Arrange
        var serviceCollection = new ServiceCollection();
        var services = GetAllServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        TController controller = await GetController(services, serviceProvider);


        //Act

        var createDto = GetCreateDtoSample();
        MutationBeforeDtoCreation(createDto, services, serviceProvider);
        CreateResponseDto create = (await controller.Create(createDto, CancellationToken) as OkObjectResult).Value as CreateResponseDto;
        var updateDto = GetUpdateDtoSample();
        updateDto.Id = create.Id;
        MutationBeforeDtoUpdate(updateDto, services, serviceProvider);
        var result = await controller.Put(updateDto, CancellationToken);


        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();


    }


    [Fact]
    public virtual async Task CrudController_Put_ReturnBadRequest()
    {
        //Arrange
        var serviceCollection = new ServiceCollection();
        var services = GetAllServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        TController controller = await GetController(services, serviceProvider);


        //Act

        var createDto = GetCreateDtoSample();
        MutationBeforeDtoCreation(createDto, services, serviceProvider);
        CreateResponseDto create = (await controller.Create(createDto, CancellationToken) as OkObjectResult).Value as CreateResponseDto;

        var updateDto = GetUpdateDtoSample();
        MutationBeforeDtoUpdate(updateDto, services, serviceProvider);
        var result = await controller.Put(updateDto, CancellationToken);


        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<BadRequestObjectResult>();
    }


    protected record AllServicesInController(TCrudService crudService, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
    {
        public TCrudService CrudService => crudService;
        public UserManager<User> UserManager => userManager;

        public RoleManager<IdentityRole<Guid>> RoleManager => roleManager;
    }


}
