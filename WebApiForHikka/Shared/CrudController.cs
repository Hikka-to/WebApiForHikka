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
    TIService crudService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor)
    : MyBaseController(mapper, httpContextAccessor),
        ICrudController<TUpdateDto, TCreateDto>
    where TModel : class, IModel
    where TUpdateDto : ModelDto
    where TIService : ICrudService<TModel>
{
    protected TIService CrudService = crudService;


    [HttpPost]
    public virtual async Task<IActionResult> Create([FromBody] TCreateDto dto, CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();


        var model = Mapper.Map<TModel>(dto);

        Guid? id = await CrudService.CreateAsync(model, cancellationToken);

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (id == null) return BadRequest(ControllerStringConstants.SomethingWentWrongDuringCreateing);

        return Ok(new CreateResponseDto { Id = (Guid)id });
    }


    [HttpDelete("{id:Guid}")]
    public virtual async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();


        await CrudService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }


    [HttpGet("{id:Guid}")]
    public virtual async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();


        var model = Mapper.Map<TGetDto>(await CrudService.GetAsync(id, cancellationToken));
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

        CkeckIfColumnsAreInModel(paginationDto, errorEndPoint);

        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var filterPagination = Mapper.Map<FilterPagination>(paginationDto);

        var paginationCollection = await CrudService.GetAllAsync(filterPagination, cancellationToken);

        var models = Mapper.Map<List<TGetDto>>(paginationCollection.Models);
        return Ok(
            new ReturnPageDto<TGetDto>
            {
                HowManyPages = (int)Math.Ceiling((double)paginationCollection.Total / filterPagination.PageSize),
                Models = models,
                Total = paginationCollection.Total,
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


        var model = Mapper.Map<TModel>(dto);
        await CrudService.UpdateAsync(model, cancellationToken);
        return NoContent();
    }

    protected virtual ErrorEndPoint ValidateRequestForUpdateEndPoint(ThingsToValidateForUpdate thingsToValidate)
    {
        var errorEndPoint = ValidateRequest(thingsToValidate);
        if (errorEndPoint.IsError) return errorEndPoint;

        var model = CrudService.Get(thingsToValidate.UpdateDto.Id);
        if (model != null) return errorEndPoint;

        errorEndPoint.BadRequestObjectResult =
            new BadRequestObjectResult(ControllerStringConstants.ModelWithThisIdDoesntExistForUpdateEndPoint)
            { StatusCode = 404 };
        return errorEndPoint;
    }


    protected void CkeckIfColumnsAreInModel(FilterPaginationDto filterDtos, ErrorEndPoint error)
    {
        var columsOfModels = typeof(TGetDto).GetProperties();

        var listOfPropertiesNames = new List<string>();


        foreach (var i in columsOfModels)
        {
            listOfPropertiesNames.Add(i.Name);
        }


        foreach (var item in filterDtos.Sorts)
        {
            if (!listOfPropertiesNames.Contains(item.Column))
            {
                error.BadRequestObjectResult = new BadRequestObjectResult($"Column with this name {item.Column} doesn't exist");
                return;
            }

        }

        foreach (var column in filterDtos.Filters)
        {
            foreach (var item in column)
            {
                if (!listOfPropertiesNames.Contains(item.Column))
                {
                    error.BadRequestObjectResult = new BadRequestObjectResult($"Column with this name {item.Column} doesn't exist");
                    return;
                }

            }
        }

    }



    protected record ThingsToValidateForUpdate : ThingsToValidateBase
    {
        public required TUpdateDto UpdateDto { get; init; }
    }
}