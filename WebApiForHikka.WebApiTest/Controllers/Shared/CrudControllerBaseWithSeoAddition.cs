using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.Test.Controller.Shared;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.Test.Controllers.Shared;

public abstract class CrudControllerBaseWithSeoAddition<TController, TCrudService,
    TModel, TIRepository,
    TUpdateDto, TCreateDto, TGetDto, TReturnPageDto>
    : BaseControllerTest
    where TController :ICrudController<TUpdateDto, TCreateDto>
    where TCrudService : CrudService<TModel, TIRepository>
    where TModel : ModelWithSeoAddition
    where TIRepository : ICrudRepository<TModel>
    where TUpdateDto : UpdateDtoWithSeoAddition
    where TReturnPageDto : ReturnPageDto<TGetDto>
{

}
