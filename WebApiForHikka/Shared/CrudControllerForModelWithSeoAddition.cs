using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Constants.Controllers;
using WebApiForHikka.Constants.Users;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;
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

    public CrudControllerForModelWithSeoAddition(ISeoAdditionService seoAdditionService, ICrudService<TModelWithSeoAddition> crudService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(crudService, mapper, httpContextAccessor)
    {
        _seoAdditionService = seoAdditionService;
    }

    [HttpGet("Create")]
    public async Task<IActionResult> Create([FromQuery] TCreateDtoWithSeoAddition dto, CancellationToken cancellationToken)
    {
        var jwt = this.GetJwtTokenAuthorizationFromHeader();
        string[] rolesToAccessTheEdnpoint = [UserStringConstants.UserRole];
        if (!this.CheckIfTheUserHasTheRightRole(jwt, rolesToAccessTheEdnpoint))
        {
            string errorMessage = ControllerStringConstants.ErrorMessageThisEndpointCanAccess
                + rolesToAccessTheEdnpoint.Aggregate((s1, s2) => s1 + ", " + s2);
            return Unauthorized(errorMessage);
        }

        var model = _mapper.Map<TModelWithSeoAddition>(dto);
        model.SeoAddition = await _seoAdditionService.GetAsync(dto.SeoAdditionId, cancellationToken);

        Guid? id = await _crudService.CreateAsync(model, cancellationToken);

        if (id == null)
        {
            return BadRequest(UserStringConstants.MessageUserIsntRegistrated);
        }

        return Ok(new CreateResponseDto() { Id = (Guid)id });
    }


    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var jwt = this.GetJwtTokenAuthorizationFromHeader();
        string[] rolesToAccessTheEdnpoint = [UserStringConstants.UserRole];
        if (!this.CheckIfTheUserHasTheRightRole(jwt, rolesToAccessTheEdnpoint))
        {
            string errorMessage = ControllerStringConstants.ErrorMessageThisEndpointCanAccess
                + rolesToAccessTheEdnpoint.Aggregate((s1, s2) => s1 + ", " + s2);
            return Unauthorized(errorMessage);
        }


        await _crudService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }


    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var jwt = this.GetJwtTokenAuthorizationFromHeader();
        string[] rolesToAccessTheEdnpoint = [UserStringConstants.UserRole];
        if (!this.CheckIfTheUserHasTheRightRole(jwt, rolesToAccessTheEdnpoint))
        {
            string errorMessage = ControllerStringConstants.ErrorMessageThisEndpointCanAccess
                + rolesToAccessTheEdnpoint.Aggregate((s1, s2) => s1 + ", " + s2);
            return Unauthorized(errorMessage);
        }


        var model = _mapper.Map<TGetDtoWithSeoAddition>(await _crudService.GetAsync(id, cancellationToken));
        if (model is null)
            return NotFound();

        return Ok(model);
    }


    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] FilterPaginationDto paginationDto, CancellationToken cancellationToken)
    {
        var jwt = this.GetJwtTokenAuthorizationFromHeader();
        string[] rolesToAccessTheEdnpoint = [UserStringConstants.UserRole];
        if (!this.CheckIfTheUserHasTheRightRole(jwt, rolesToAccessTheEdnpoint))
        {
            string errorMessage = ControllerStringConstants.ErrorMessageThisEndpointCanAccess
                + rolesToAccessTheEdnpoint.Aggregate((s1, s2) => s1 + ", " + s2);
            return Unauthorized(errorMessage);
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


    [HttpPut("update")]
    public async Task<IActionResult> Put([FromBody] TUpdateDtoWithSeoAddition dto, CancellationToken cancellationToken)
    {
        var jwt = this.GetJwtTokenAuthorizationFromHeader();
        string[] rolesToAccessTheEdnpoint = [UserStringConstants.UserRole];
        if (!this.CheckIfTheUserHasTheRightRole(jwt, rolesToAccessTheEdnpoint))
        {
            string errorMessage = ControllerStringConstants.ErrorMessageThisEndpointCanAccess
                + rolesToAccessTheEdnpoint.Aggregate((s1, s2) => s1 + ", " + s2);
            return Unauthorized(errorMessage);
        }

        var model = _mapper.Map<TModelWithSeoAddition>(dto);
        if (!ModelState.IsValid)
        {
            return BadRequest(GetAllErrorsDuringValidation());
        }

        model.SeoAddition = await _seoAdditionService.GetAsync(dto.SeoAdditionId, cancellationToken);


        await _crudService.UpdateAsync(model, cancellationToken);
        return NoContent();
    }
}
