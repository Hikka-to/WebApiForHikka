﻿using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApiForHikka.Domain;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.Test.Controller.Shared;

public abstract class CrudControllerBaseTest<TController, TUpdateDto, TCreateDto, TGetDto, TReturnPageDto>
    : BaseControllerTest
    where TController : ICrudController<TUpdateDto, TCreateDto>

{
    protected abstract TController GetController();
    protected abstract TCreateDto GetCreateDtoSample();
    protected abstract TUpdateDto GetUpdateDtoSample();

    public virtual async Task CrudController_Create_ReturnsCreateResponseDto()
    {
        //Arrange
        TController controller = GetController();

        //Act

        var result = await controller.Create(GetCreateDtoSample(), _cancellationToken);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<CreateResponseDto>();


    }

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

    public virtual async Task CrudController_Get_ReturnsGetDto()
    {
        //Arrange
        TController controller = GetController();

        //Act

        var createResponse = await controller.Create(GetCreateDtoSample(), _cancellationToken) as CreateResponseDto;

        var result = await controller.Get(createResponse.Id, _cancellationToken);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<TGetDto>();


    }

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

    //public virtual Task<IActionResult> CrudController_Put_()
    //{
    //    //Arrange
    //    TController controller = GetController();

    //    //Act

    //    var result = await controller.Put(GetUpdateDtoSample, _cancellationToken);

    //    //Assert
    //    result.Should().NotBeNull();
    //    result.Should().BeOfType<TReturnPageDto>();
    //}
}
