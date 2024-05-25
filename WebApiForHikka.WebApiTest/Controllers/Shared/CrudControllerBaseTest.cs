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
    where TModel : Model
    where TIRepository : ICrudRepository<TModel>
    where TUpdateDto : ModelDto
    where TReturnPageDto : ReturnPageDto<TGetDto>

{

    protected abstract TController GetController(TCrudService crudService);
    protected abstract TCreateDto GetCreateDtoSample();
    protected abstract TUpdateDto GetUpdateDtoSample();
    protected abstract TGetDto GetGetDtoSample();
    protected abstract TModel GetModelSample();
    protected abstract ICollection<TModel> GetCollectionOfModels(int howMany);
    protected abstract TCrudService GetCrudService();



    [Fact]
    public virtual async Task CrudController_Get_ReturnsNotFound() 
    {
        //Arrange
        var service = GetCrudService();
        TController controller = GetController(service);

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
        var service = GetCrudService();
        TController controller = GetController(service);
        foreach (var item in GetCollectionOfModels(10))
        {
            await service.CreateAsync(item, _cancellationToken);
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
        var service = GetCrudService();
        TController controller = GetController(service);

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
        var service = GetCrudService();
        var id = await service.CreateAsync(GetModelSample(), _cancellationToken);
        TController controller = GetController(service);

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
        var service = GetCrudService();
        TController controller = GetController(service);

        var model = GetModelSample();
        var id = await service.CreateAsync(model, _cancellationToken);

        //Act

        var result = await controller.Get(id, _cancellationToken);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }
    

    [Fact]
    public virtual async Task CrudController_Put_ReturnNoContent()
    {
        //Arrange
        var service = GetCrudService();
        TController controller = GetController(service);


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
        var service = GetCrudService();
        TController controller = GetController(service);


        //Act

        var createDto = GetCreateDtoSample();

        CreateResponseDto create = (await controller.Create(createDto, _cancellationToken) as OkObjectResult).Value as CreateResponseDto;

        var updateDto = GetUpdateDtoSample();

        var result = await controller.Put(updateDto, _cancellationToken);


        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<BadRequestObjectResult>();
    }


}
