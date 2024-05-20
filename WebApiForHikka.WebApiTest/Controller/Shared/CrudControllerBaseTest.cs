using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApiForHikka.Domain;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.Test.Controller.Shared;

public abstract class CrudControllerBaseTest<TController, TUpdateDto, TCreateDto>
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

    public virtual Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public virtual Task<IActionResult> GetAll([FromQuery] FilterPaginationDto paginationDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public virtual Task<IActionResult> Put([FromBody] TUpdateDto dto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
