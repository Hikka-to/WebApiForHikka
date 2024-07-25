using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Constants.Controllers;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.SharedDtos;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.WebApi.Shared.ErrorEndPoints;

namespace WebApiForHikka.WebApi.Shared;

// ! Don't change TModel position
[Authorize(Policy = ControllerStringConstants.CanAccessOnlyAdmin)]
public abstract class CrudController<TGetDto, TUpdateDto, TCreateDto, TIService, TModel>(
    TIService crudRelationService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor)
    : MyBaseController(mapper, httpContextAccessor),
        ICrudController<TUpdateDto, TCreateDto>
    where TModel : class, IModel
    where TUpdateDto : ModelDto
    where TIService : ICrudService<TModel>
{
    protected TIService CrudRelationService = crudRelationService;


    [HttpPost]
    public virtual async Task<IActionResult> Create([FromBody] TCreateDto dto, CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();


        var model = _mapper.Map<TModel>(dto);

        Guid? id = await CrudRelationService.CreateAsync(model, cancellationToken);

        if (id == null) return BadRequest(ControllerStringConstants.SomethingWentWrongDuringCreateing);

        return Ok(new CreateResponseDto { Id = (Guid)id });
    }


    [HttpDelete("{id:Guid}")]
    public virtual async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();


        await CrudRelationService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }


    [HttpGet("{id:Guid}")]
    public virtual async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();


        var model = _mapper.Map<TGetDto>(await CrudRelationService.GetAsync(id, cancellationToken));
        if (model is null)
            return NotFound();

        return Ok(model);
    }


    [HttpPost("GetAll")]
    public virtual async Task<IActionResult> GetAll([FromBody] FilterPaginationDto paginationDto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var filterPagination = _mapper.Map<FilterPagination>(paginationDto);

        var paginationCollection = await CrudRelationService.GetAllAsync(filterPagination, cancellationToken);

        var models = _mapper.Map<List<TGetDto>>(paginationCollection.Models);
        return Ok(
            new ReturnPageDto<TGetDto>
            {
                HowManyPages = (int)Math.Ceiling((double)paginationCollection.Total / filterPagination.PageSize),
                Models = models
            }
        );
    }


    [HttpPut]
    public virtual async Task<IActionResult> Put([FromBody] TUpdateDto dto, CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequestForUpdateEndPoint(
            new ThingsToValidateForUpdate
            {
                UpdateDto = dto
            });

        if (errorEndPoint.IsError) return errorEndPoint.GetError();


        var model = _mapper.Map<TModel>(dto);
        await CrudRelationService.UpdateAsync(model, cancellationToken);
        return NoContent();
    }

    protected virtual ErrorEndPoint ValidateRequestForUpdateEndPoint(ThingsToValidateForUpdate thingsToValidate)
    {
        var errorEndPoint = ValidateRequest(thingsToValidate);
        if (errorEndPoint.IsError) return errorEndPoint;

        var model = CrudRelationService.Get(thingsToValidate.UpdateDto.Id);
        if (model != null) return errorEndPoint;

        errorEndPoint.BadRequestObjectResult =
            new BadRequestObjectResult(ControllerStringConstants.ModelWithThisIdDoesntExistForUpdateEndPoint)
                { StatusCode = 404 };
        return errorEndPoint;
    }

    protected record ThingsToValidateForUpdate : ThingsToValidateBase
    {
        public required TUpdateDto UpdateDto { get; init; }
    }
}