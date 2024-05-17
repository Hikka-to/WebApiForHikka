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
        var jwt = this.GetJwtTokenAuthorizationFromHeader();
        string[] rolesToAccessTheEndpoint = [UserStringConstants.AdminRole];
        if (!this.CheckIfTheUserHasTheRightRole(jwt, rolesToAccessTheEndpoint))
        {
            string errorMessage = ControllerStringConstants.ErrorMessageThisEndpointCanAccess
                + string.Join(", ", rolesToAccessTheEndpoint);
            return Unauthorized(errorMessage);
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(GetAllErrorsDuringValidation());
        }
        var model = _mapper.Map<Tag>(dto);

        model.SeoAddition = await _seoAdditionService.GetAsync(dto.SeoAdditionId, cancellationToken);

        if (dto.ParentTagId != null) 
        {
            model.ParentTag = await _crudService.GetAsync((Guid)dto.ParentTagId, cancellationToken);
        }

        var createdId = await _crudService.CreateAsync(model, cancellationToken);


        return Ok(new CreateResponseDto() { Id = createdId });
    }

    public override async Task<IActionResult> Put([FromBody] UpdateTagDto dto, CancellationToken cancellationToken)
    {
        var jwt = this.GetJwtTokenAuthorizationFromHeader();
        string[] rolesToAccessTheEndpoint = [UserStringConstants.AdminRole];
        if (!this.CheckIfTheUserHasTheRightRole(jwt, rolesToAccessTheEndpoint))
        {
            string errorMessage = ControllerStringConstants.ErrorMessageThisEndpointCanAccess
                + string.Join(", ", rolesToAccessTheEndpoint);
            return Unauthorized(errorMessage);
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(GetAllErrorsDuringValidation());
        }
        var model = _mapper.Map<Tag>(dto);

        model.SeoAddition = await _seoAdditionService.GetAsync(dto.SeoAdditionId, cancellationToken);

        if (dto.ParentTagId != null)
        {
            model.ParentTag = await _crudService.GetAsync((Guid)dto.ParentTagId, cancellationToken);
        }

        try
        {
            await _crudService.UpdateAsync(model, cancellationToken);

            return Ok(SharedStringConstants.ModelUpdatedSuccessfully);
        }
        catch (Exception)
        {
            return BadRequest(SharedStringConstants.SomethingWentWrongDuringUpdateing);
        }


    }
}
