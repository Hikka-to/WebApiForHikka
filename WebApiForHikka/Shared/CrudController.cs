using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Constants.Controllers;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Users;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.WebApi.Shared;

public abstract class CrudController
    <TGetDto, TUpdateDto, TCreateDto, TIService, TModel>
    : MyBaseController, ICrudController<TUpdateDto, TCreateDto>
    where TModel  : Model
    where TIService : ICrudService<TModel>
{
    protected TIService _crudService;

    public CrudController(TIService crudService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, httpContextAccessor) 
    {
        _crudService = crudService;

    }


    [HttpPost("Create")]
    public virtual async Task<IActionResult> Create([FromBody] TCreateDto dto, CancellationToken cancellationToken)
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


        var model = _mapper.Map<TModel>(dto);

        Guid? id = await _crudService.CreateAsync(model, cancellationToken);

        if (id == null)
        {
            return BadRequest(SharedStringConstants.SomethingWentWrongDuringCreateing);
        }

        return Ok(new CreateResponseDto() {  Id = (Guid)id });
    }


    [HttpDelete("{id:Guid}")]
    public virtual async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
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
    public virtual async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var jwt = this.GetJwtTokenAuthorizationFromHeader();
        string[] rolesToAccessTheEndpoint = [UserStringConstants.UserRole, UserStringConstants.AdminRole];
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


        var model = _mapper.Map<TGetDto>(await _crudService.GetAsync(id, cancellationToken));
        if (model is null)
            return NotFound();

        return Ok(model);
    }


    [HttpGet("GetAll")]
    public virtual async Task<IActionResult> GetAll([FromQuery] FilterPaginationDto paginationDto, CancellationToken cancellationToken)
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

        var model = _mapper.Map<TModel>(dto);
                await _crudService.UpdateAsync(model, cancellationToken);
        return NoContent();
    }
}
