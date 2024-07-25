using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.Relation.Notifications;
using WebApiForHikka.Application.WithoutSeoAddition.Resources;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Dtos.Dto.Relation.Notifications;
using WebApiForHikka.Dtos.Dto.Relation.Relateds;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.Relation;

public class NotificationController(
    INotificationRelationService crudRelationService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    IResourceService resourceService
    )
    : CrudController<
        GetNotificationDto,
        UpdateNotificationDto,
        CreateNotificationDto,
        INotificationRelationService,
        Notification
    >(crudRelationService, mapper, httpContextAccessor)
{
     public override async Task<IActionResult> Create([FromBody] CreateNotificationDto dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = _mapper.Map<Notification>(dto);

        model.Resource = (await resourceService.GetAsync(dto.ResourceId, cancellationToken))!;

        var createdId = await CrudRelationService.CreateAsync(model, cancellationToken);

        return Ok(new CreateResponseDto { Id = createdId });
    }

    public override async Task<IActionResult> Put([FromBody] UpdateNotificationDto dto, CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequestForUpdateEndPoint(new ThingsToValidateForUpdate
        {
            UpdateDto = dto
        });
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = _mapper.Map<Notification>(dto);

        model.Resource = (await resourceService.GetAsync(dto.ResourceId, cancellationToken))!;

        await CrudRelationService.UpdateAsync(model, cancellationToken);

        return NoContent();
    }

}