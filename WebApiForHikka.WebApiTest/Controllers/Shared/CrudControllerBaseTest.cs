using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;
using System.Threading;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Users;
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
    where TModel : IModel
    where TIRepository : ICrudRepository<TModel>
    where TUpdateDto : ModelDto
    where TReturnPageDto : ReturnPageDto<TGetDto>

{

    protected abstract TController GetController(AllServicesInController allServicesInController);
    protected abstract TCreateDto GetCreateDtoSample();
    protected abstract TUpdateDto GetUpdateDtoSample();
    protected abstract TGetDto GetGetDtoSample();
    protected abstract TModel GetModelSample();
    protected abstract AllServicesInController GetAllServices();
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
        TController controller = GetController(GetAllServices());

        //Act

        var result = await controller.Get(new Guid(), _cancellationToken);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NotFoundResult>();

    }


    [Fact]
    public virtual async Task CrudController_GetAll_ReturnsReturnPageDto() 
    {
        //Arrange
        var services = GetAllServices();
        TController controller = GetController(services);
        foreach (var item in GetCollectionOfModels(10))
        {
            await services.CrudService.CreateAsync(item, _cancellationToken);
        }

        //Act

        var result = await controller.GetAll(_filterPaginationDto, _cancellationToken) as OkObjectResult;


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
        var services = GetAllServices();
        TController controller = GetController(services);

        //Act
        var result = await controller.Create(GetCreateDtoSample(), _cancellationToken) as OkObjectResult;

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
        var services = GetAllServices();
        var id = await services.CrudService.CreateAsync(GetModelSample(), _cancellationToken);
        TController controller = GetController(services);

        //Act
        var result = await controller.Delete(id, _cancellationToken);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();

    }


    [Fact]
    public virtual async Task CrudController_Get_ReturnsOkObjectResult()
    {
        //Arrange
        var services = GetAllServices();
        TController controller = GetController(services);

        var model = GetModelSample();
        var id = await services.CrudService.CreateAsync(model, _cancellationToken);

        //Act

        var result = await controller.Get(id, _cancellationToken);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }
    

    [Fact]
    public virtual async Task CrudController_Put_ReturnsNoContent()
    {
        //Arrange
        var services = GetAllServices();
        TController controller = GetController(services);


        //Act

        var createDto = GetCreateDtoSample();

        CreateResponseDto create = (await controller.Create(createDto, _cancellationToken) as OkObjectResult).Value as CreateResponseDto;
        var updateDto = GetUpdateDtoSample();
        updateDto.Id = create.Id;

        var result = await controller.Put(updateDto, _cancellationToken);


        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();


    }


    [Fact]
    public virtual async Task CrudController_Put_ReturnBadRequest()
    {
        //Arrange
        var services = GetAllServices();
        TController controller = GetController(services);


        //Act

        var createDto = GetCreateDtoSample();

        CreateResponseDto create = (await controller.Create(createDto, _cancellationToken) as OkObjectResult).Value as CreateResponseDto;

        var updateDto = GetUpdateDtoSample();

        var result = await controller.Put(updateDto, _cancellationToken);


        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<BadRequestObjectResult>();
    }


    protected record AllServicesInController(TCrudService crudService) 
    {
        public TCrudService CrudService = crudService;
    }


}
