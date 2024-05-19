using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithSeoAddition.Tags;
using WebApiForHikka.Constants.Controllers;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Tags;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.WebApi.Shared;
using WebApiForHikka.WebApi.Shared.ErrorEndPoints;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

public class TagController : CrudControllerForModelWithSeoAddition<GetTagDto,
    UpdateTagDto,
    CreateTagDto,
    ITagService,
    Tag
    >
{
    public TagController(ITagService crudService, ISeoAdditionService seoAdditionService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(crudService, seoAdditionService, mapper, httpContextAccessor)
    {
    }

    public override async Task<IActionResult> Create([FromBody] CreateTagDto dto, CancellationToken cancellationToken)
    {
        string[] rolesToAccessTheEndpoint = [UserStringConstants.AdminRole];
        ErrorEndPoint errorEndPoint = this.ValidateRequest(new ThingsToValidateBase() {
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

    public override async Task<IActionResult> Put([FromBody] UpdateTagDto dto, CancellationToken cancellationToken)
    {
        string[] rolesToAccessTheEndpoint = [UserStringConstants.AdminRole];

        ErrorEndPoint errorEndPoint = this.ValidateRequest(new ThingsToValidateBase() 
        {
            RolesToAccessTheEndPoint = rolesToAccessTheEndpoint,
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

        try
        {
            await _crudService.UpdateAsync(model, cancellationToken);

            return Ok(ControllerStringConstants.ModelUpdatedSuccessfully);
        }
        catch (Exception)
        {
            return BadRequest(ControllerStringConstants.SomethingWentWrongDuringUpdateing);
        }


    }
}
