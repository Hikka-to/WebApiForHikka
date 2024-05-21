using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
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

{

    protected readonly TCrudService _crudService = A.Fake<TCrudService>();


    protected abstract TController GetController();
    protected abstract TCreateDto GetCreateDtoSample();
    protected abstract TUpdateDto GetUpdateDtoSample();
    protected abstract TGetDto GetGetDtoSample();
    protected abstract TModel GetModelSample();

    [Fact]
    public virtual async Task CrudController_Create_ReturnsCreateResponseDto()
    {
        //Arrange
        TController controller = GetController();

        //Act

        var result = await controller.Create(GetCreateDtoSample(), _cancellationToken);

        //Assert
        result.Should().NotBeNull();
        result.Should().NotBeOfType<OkObjectResult>();
    }


    [Fact]
    public virtual async Task CrudController_Delete_ReturnsNoContent()
    {
        //Arrange
        TController controller = GetController();

        //Act

        var createResponse = await controller.Create(GetCreateDtoSample(), _cancellationToken) as CreateResponseDto;

        var result = await controller.Delete(createResponse.Id, _cancellationToken);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();

    }


    [Fact]
    public virtual async Task CrudController_Get_ReturnsOkObjectResult()
    {
        //Arrange
        TController controller = GetController();
        var model = GetModelSample();
        A.CallTo(() => _crudService.GetAsync(model.Id, A<CancellationToken>.Ignored)).Returns(model);
        A.CallTo(() => _mapper.Map<TGetDto>(model)).Returns(GetGetDtoSample());

        //Act

        var result = await controller.Get(model.Id, _cancellationToken);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public virtual async Task CrudController_Get_ReturnsBadRequest()
    {
        //Arrange
        TController controller = GetController();
        var model = GetModelSample();
        A.CallTo(() => _crudService.GetAsync(model.Id, A<CancellationToken>.Ignored)).Returns((TModel?)null);
        //A.CallTo(() => _mapper.Map<TGetDto>(model)).Returns(null);
        //Act

        var result = await controller.Get(model.Id, _cancellationToken);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NotFound>();
    }

    [Fact]
    public virtual async Task CrudController_GetAll_ReturnsPageDto()
    {
        //Arrange
        TController controller = GetController();

        //Act

        var result = await controller.GetAll(_filterPaginationDto, _cancellationToken);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<TReturnPageDto>();
    }

    [Fact]
    public virtual async Task CrudController_Put_ReturnNoContent()
    {
        //Arrange
        TController controller = GetController();

        //Act

        var createDto = GetCreateDtoSample();

        var create = await controller.Create(createDto, _cancellationToken) as CreateResponseDto;

        var updateDto = GetUpdateDtoSample();
        updateDto.Id = create.Id;

        var result = await controller.Put(updateDto, _cancellationToken);

        var updateResualt = await controller.Get(updateDto.Id, _cancellationToken);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();
        updateResualt.Should().BeEquivalentTo(updateDto);
    }
}
