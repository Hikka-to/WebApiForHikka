using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithSeoAddition.Tags;
using WebApiForHikka.Constants.Controllers;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Tags;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.WebApi.Shared;
using WebApiForHikka.WebApi.Shared.ErrorEndPoints;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

public class TagController
    (ITagService crudService, ISeoAdditionService seoAdditionService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    : CrudControllerForModelWithSeoAddition<GetTagDto,
    UpdateTagDto,
    CreateTagDto,
    ITagService,
    Tag
    >(crudService, seoAdditionService, mapper, httpContextAccessor)
{

    [HttpPost("Create")]
    public override async Task<IActionResult> Create([FromBody] CreateTagDto dto, CancellationToken cancellationToken)
    {
        string[] rolesToAccessTheEndpoint = [UserStringConstants.AdminRole];
        ErrorEndPoint errorEndPoint = ValidateRequest(new ThingsToValidateBase()
        {
            RolesToAccessTheEndPoint = rolesToAccessTheEndpoint,
        });
        if (errorEndPoint.IsError)
        {
            return errorEndPoint.GetError();
        }


        var model = _mapper.Map<Tag>(dto);

        var seoAddition = _mapper.Map<SeoAddition>(dto.SeoAddition);
        await _seoAdditionService.CreateAsync(seoAddition, cancellationToken);

        model.SeoAddition = seoAddition;

        if (dto.ParentTagId != null)
        {
            model.ParentTag = await _crudService.GetAsync((Guid)dto.ParentTagId, cancellationToken);
        }

        var createdId = await _crudService.CreateAsync(model, cancellationToken);


        return Ok(new CreateResponseDto() { Id = createdId });
    }


    [HttpPut("Update")]
    public override async Task<IActionResult> Put([FromBody] UpdateTagDto dto, CancellationToken cancellationToken)
    {
        string[] rolesToAccessTheEndpoint = [UserStringConstants.AdminRole];

        ErrorEndPoint errorEndPoint = this.ValidateRequestForUpdateWithSeoAddtionEndPoint(new ThingsToValidateWithSeoAdditionForUpdate()
        {
            RolesToAccessTheEndPoint = rolesToAccessTheEndpoint,
            UpdateDto = dto,
            IdForSeoAddition = dto.SeoAddition.Id,

        });
        if (errorEndPoint.IsError)
        {
            return errorEndPoint.GetError();
        }


        var model = _mapper.Map<Tag>(dto);

        model.SeoAddition = _mapper.Map<SeoAddition>(dto.SeoAddition);

        if (dto.ParentTagId != null)
        {
            model.ParentTag = await _crudService.GetAsync((Guid)dto.ParentTagId, cancellationToken);
        }

        await _crudService.UpdateAsync(model, cancellationToken);

        return NoContent();
    }
}