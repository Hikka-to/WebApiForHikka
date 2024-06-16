using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Constants.Controllers;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.WebApi.Shared.ErrorEndPoints;

namespace WebApiForHikka.WebApi.Shared;

[Authorize(Policy = ControllerStringConstants.CanAccessOnlyAdmin)]
public abstract class CrudController<TGetDto, TUpdateDto, TCreateDto, TIService, TModel>
    (TIService crudService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    : MyBaseController(mapper, httpContextAccessor),
    ICrudController<TUpdateDto, TCreateDto>
    where TModel : class, IModel
    where TUpdateDto : ModelDto
    where TIService : ICrudService<TModel>

{
    protected TIService _crudService = crudService;

    [HttpPost("Create")]
    public virtual async Task<IActionResult> Create([FromBody] TCreateDto dto, CancellationToken cancellationToken)
    {
        ErrorEndPoint errorEndPoint = ValidateRequest(
            new ThingsToValidateBase()
            {
            });
        if (errorEndPoint.IsError)
        {
            return errorEndPoint.GetError();
        }


        var model = _mapper.Map<TModel>(dto);

        Guid? id = await _crudService.CreateAsync(model, cancellationToken);

        if (id == null)
        {
            return BadRequest(ControllerStringConstants.SomethingWentWrongDuringCreateing);
        }

        return Ok(new CreateResponseDto() { Id = (Guid)id });
    }


    [HttpDelete("{id:Guid}")]
    public virtual async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        ErrorEndPoint errorEndPoint = ValidateRequest(
            new ThingsToValidateBase()
            {
            });
        if (errorEndPoint.IsError)
        {
            return errorEndPoint.GetError();
        }

        await _crudService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }


    [HttpGet("{id:Guid}")]
    public virtual async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {

        ErrorEndPoint errorEndPoint = ValidateRequest(
            new ThingsToValidateBase()
            {
            });
        if (errorEndPoint.IsError)
        {
            return errorEndPoint.GetError();
        }


        var model = _mapper.Map<TGetDto>(await _crudService.GetAsync(id, cancellationToken));
        if (model is null)
            return NotFound();

        return Ok(model);
    }


    [HttpGet("GetAll")]
    public virtual async Task<IActionResult> GetAll([FromQuery] FilterPaginationDto paginationDto, CancellationToken cancellationToken)
    {
        ErrorEndPoint errorEndPoint = ValidateRequest(
            new ThingsToValidateBase()
            {
            });
        if (errorEndPoint.IsError)
        {
            return errorEndPoint.GetError();
        }

        var paginationCollection = await _crudService.GetAllAsync(paginationDto, cancellationToken);

        var models = _mapper.Map<List<TGetDto>>(paginationCollection.Models);
        return Ok(
            new ReturnPageDto<TGetDto>()
            {
                HowManyPages = (int)Math.Ceiling((double)paginationCollection.Total / paginationDto.PageSize),
                Models = models,
            }

        );
    }


    [HttpPut("Update")]
    public virtual async Task<IActionResult> Put([FromBody] TUpdateDto dto, CancellationToken cancellationToken)
    {
        ErrorEndPoint errorEndPoint = ValidateRequestForUpdateEndPoint(
            new ThingsToValidateForUpdate()
            {
                UpdateDto = dto
            });

        if (errorEndPoint.IsError)
        {
            return errorEndPoint.GetError();
        }


        var model = _mapper.Map<TModel>(dto);
        await _crudService.UpdateAsync(model, cancellationToken);
        return NoContent();
    }

    protected virtual ErrorEndPoint ValidateRequestForUpdateEndPoint(ThingsToValidateForUpdate thingsToValidate)
    {
        ErrorEndPoint errorEndPoint = ValidateRequest(thingsToValidate);
        if (errorEndPoint.IsError)
        {
            return errorEndPoint;
        }

        var model = _crudService.Get(thingsToValidate.UpdateDto.Id);
        if (model == null)
        {
            errorEndPoint.BadRequestObjectResult = BadRequest(ControllerStringConstants.ModelWithThisIdDoesntExistForUpdateEndPoint);
            return errorEndPoint;
        }


        return errorEndPoint;
    }

    protected record ThingsToValidateForUpdate : ThingsToValidateBase
    {
        public required TUpdateDto UpdateDto { get; init; }
    }
}