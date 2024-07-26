﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Constants.Controllers;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.SharedDtos;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.WebApi.Shared.ErrorEndPoints;

namespace WebApiForHikka.WebApi.Shared;

public abstract class CrudControllerForModelWithSeoAddition
    <TGetDtoWithSeoAddition, TUpdateDtoWithSeoAddition, TCreateDtoWithSeoAddition, TIService, TModelWithSeoAddition>(
        TIService crudService,
        ISeoAdditionService seoAdditionService,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    : CrudController<
        TGetDtoWithSeoAddition,
        TUpdateDtoWithSeoAddition,
        TCreateDtoWithSeoAddition,
        TIService,
        TModelWithSeoAddition
    >(crudService, mapper, httpContextAccessor)
    where TModelWithSeoAddition : class, IModelWithSeoAddition
    where TGetDtoWithSeoAddition : GetDtoWithSeoAddition
    where TUpdateDtoWithSeoAddition : UpdateDtoWithSeoAddition
    where TCreateDtoWithSeoAddition : CreateDtoWithSeoAddition
    where TIService : ICrudService<TModelWithSeoAddition>
{
    protected readonly ISeoAdditionService SeoAdditionService = seoAdditionService;

    [HttpPost]
    public override async Task<IActionResult> Create([FromBody] TCreateDtoWithSeoAddition dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = Mapper.Map<TModelWithSeoAddition>(dto);
        var seoAddition = Mapper.Map<SeoAddition>(dto.SeoAddition);
        await SeoAdditionService.CreateAsync(seoAddition, cancellationToken);
        model.SeoAddition = seoAddition;

        Guid? id = await CrudRelationService.CreateAsync(model, cancellationToken);

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (id == null) return BadRequest(ControllerStringConstants.SomethingWentWrongDuringCreateing);

        return Ok(new CreateResponseDto { Id = (Guid)id });
    }


    [HttpDelete("{id:Guid}")]
    public override async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = await CrudRelationService.GetAsync(id, cancellationToken);

        if (model is null)
            return NoContent();


        await CrudRelationService.DeleteAsync(model.Id, cancellationToken);
        await SeoAdditionService.DeleteAsync(model.SeoAddition.Id, cancellationToken);
        return NoContent();
    }


    [HttpGet("{id:Guid}")]
    public override async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = Mapper.Map<TGetDtoWithSeoAddition>(await CrudRelationService.GetAsync(id, cancellationToken));
        if (model is null)
            return NotFound();

        return Ok(model);
    }


    [HttpPost("GetAll")]
    public override async Task<IActionResult> GetAll([FromBody] FilterPaginationDto paginationDto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var filterPagination = Mapper.Map<FilterPagination>(paginationDto);

        var paginationCollection = await CrudRelationService.GetAllAsync(filterPagination, cancellationToken);

        var models = Mapper.Map<List<TGetDtoWithSeoAddition>>(paginationCollection.Models);
        return Ok(
            new ReturnPageDto<TGetDtoWithSeoAddition>
            {
                HowManyPages = (int)Math.Ceiling((double)paginationCollection.Total / filterPagination.PageSize),
                Models = models
            }
        );
    }


    [HttpPut]
    public override async Task<IActionResult> Put([FromBody] TUpdateDtoWithSeoAddition dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequestForUpdateWithSeoAddtionEndPoint(
            new ThingsToValidateWithSeoAdditionForUpdate
            {
                UpdateDto = dto,
                IdForSeoAddition = dto.SeoAddition.Id
            }
        );
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = Mapper.Map<TModelWithSeoAddition>(dto);
        var seoAdditionModel = Mapper.Map<SeoAddition>(dto.SeoAddition);
        await SeoAdditionService.UpdateAsync(seoAdditionModel, cancellationToken);

        var seoAddition = await SeoAdditionService.GetAsync(seoAdditionModel.Id, cancellationToken);
        model.SeoAddition = seoAddition!;

        await CrudRelationService.UpdateAsync(model, cancellationToken);
        return NoContent();
    }


    protected ErrorEndPoint ValidateRequestForUpdateWithSeoAddtionEndPoint(
        ThingsToValidateWithSeoAdditionForUpdate thingsToValidate)
    {
        var errorEndPoint = base.ValidateRequestForUpdateEndPoint(thingsToValidate);

        if (errorEndPoint.IsError) return errorEndPoint;

        if (thingsToValidate.UpdateDto.SeoAddition.Id != thingsToValidate.IdForSeoAddition)
        {
            errorEndPoint.BadRequestObjectResult =
                BadRequest(ControllerStringConstants.SeoAdditionDoesntConnectToTheModel);
            return errorEndPoint;
        }

        return errorEndPoint;
    }

    protected record ThingsToValidateWithSeoAdditionForUpdate : ThingsToValidateForUpdate
    {
        public required Guid IdForSeoAddition { get; init; }
    }
}