using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Constants.Controllers;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.WebApi.Shared;
public abstract class CrudControllerForModelWithSeoAddition<TGetDtoWithSeoAddition, TUpdateDtoWithSeoAddition, TCreateDtoWithSeoAddition, TIService, TModelWithSeoAddition> : CrudController<
    TGetDtoWithSeoAddition,
    TUpdateDtoWithSeoAddition,
    TCreateDtoWithSeoAddition,
    TIService,
    TModelWithSeoAddition
    >
    where TModelWithSeoAddition : ModelWithSeoAddition
    where TGetDtoWithSeoAddition : GetDtoWithSeoAddition
    where TUpdateDtoWithSeoAddition : UpdateDtoWithSeoAddition
    where TCreateDtoWithSeoAddition : CreateDtoWithSeoAddition
    where TIService : ICrudService<TModelWithSeoAddition>
{
    protected ISeoAdditionService _seoAdditionService;

    public CrudControllerForModelWithSeoAddition(TIService crudService, ISeoAdditionService seoAdditionService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(crudService, mapper, httpContextAccessor)
    {
        _seoAdditionService = seoAdditionService;
    }

    [HttpPost("Create")]
    public override async Task<IActionResult> Create([FromBody] TCreateDtoWithSeoAddition dto, CancellationToken cancellationToken)
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



        var model = _mapper.Map<TModelWithSeoAddition>(dto);
        model.SeoAddition = await _seoAdditionService.GetAsync(dto.SeoAdditionId, cancellationToken);

        Guid? id = await _crudService.CreateAsync(model, cancellationToken);

        if (id == null)
        {
            return BadRequest(SharedStringConstants.SomethingWentWrongDuringCreateing);
        }

        return Ok(new CreateResponseDto() { Id = (Guid)id });
    }


    [HttpDelete("{id:Guid}")]
    public override async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
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



        await _crudService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }


    [HttpGet("{id:Guid}")]
    public override async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
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


        var model = _mapper.Map<TGetDtoWithSeoAddition>(await _crudService.GetAsync(id, cancellationToken));
        if (model is null)
            return NotFound();

        return Ok(model);
    }


    [HttpGet("GetAll")]
    public override async Task<IActionResult> GetAll([FromQuery] FilterPaginationDto paginationDto, CancellationToken cancellationToken)
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

        var paginationCollection = await _crudService.GetAllAsync(paginationDto, cancellationToken);

        var models = _mapper.Map<List<TGetDtoWithSeoAddition>>(paginationCollection.Models);
        return Ok(
            new ReturnPageDto<TGetDtoWithSeoAddition>()
            {
                HowManyPages = (int)Math.Ceiling((double)paginationCollection.Total / paginationDto.PageSize),
                Models = models,
            }

        );
    }


    [HttpPut("Update")]
    public override async Task<IActionResult> Put([FromBody] TUpdateDtoWithSeoAddition dto, CancellationToken cancellationToken)
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

        var model = _mapper.Map<TModelWithSeoAddition>(dto);
        

        model.SeoAddition = await _seoAdditionService.GetAsync(dto.SeoAdditionId, cancellationToken);


        await _crudService.UpdateAsync(model, cancellationToken);
        return NoContent();
    }
}
