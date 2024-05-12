using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Constants.Controllers;
using WebApiForHikka.Constants.Users;
using WebApiForHikka.Controllers;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Users;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.WebApi.Shared;

public abstract class CrudController
    <TGetDto, TUpdateDto, TCreateDto, TIService, TModel>
    : MyBaseController, ICrudController<TUpdateDto, TCreateDto>
    where TModel  : Model
    where TIService : ICrudService<TModel>
{
    protected ICrudService<TModel> _crudService;

    public CrudController(ICrudService<TModel> crudService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, httpContextAccessor) 
    {
        _crudService = crudService;

    }


    [HttpGet("Create")]
    public async Task<IActionResult> Create([FromQuery] TCreateDto dto, CancellationToken cancellationToken)
    {
        var jwt = this.GetJwtTokenAuthorizationFromHeader();
        string[] rolesToAccessTheEdnpoint = [UserStringConstants.UserRole];
        if (!this.CheckIfTheUserHasTheRightRole(jwt, rolesToAccessTheEdnpoint))
        {
            string errorMessage = ControllerStringConstants.ErrorMessageThisEndpointCanAccess
                + rolesToAccessTheEdnpoint.Aggregate((s1, s2) => s1 + ", " + s2);
            return Unauthorized(errorMessage);
        }

        var model = _mapper.Map<TModel>(dto);

        Guid? id = await _crudService.CreateAsync(model, cancellationToken);

        if (id == null)
        {
            return BadRequest(UserStringConstants.MessageUserIsntRegistrated);
        }

        return Ok(new CreateResponseDto() {  Id = (Guid)id });
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


        var model = _mapper.Map<TGetDto>(await _crudService.GetAsync(id, cancellationToken));
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

        var models = _mapper.Map<List<TGetDto>>(paginationCollection.Models);
        return Ok(
            new ReturnPageDto<TGetDto>()
            {
                HowManyPages = (int)Math.Ceiling((double)paginationCollection.Total / paginationDto.PageSize),
                Models = models,
            }

        );
    }


    [HttpPut("update")]
    public async Task<IActionResult> Put([FromBody] TUpdateDto dto, CancellationToken cancellationToken)
    {
        var jwt = this.GetJwtTokenAuthorizationFromHeader();
        string[] rolesToAccessTheEdnpoint = [UserStringConstants.UserRole];
        if (!this.CheckIfTheUserHasTheRightRole(jwt, rolesToAccessTheEdnpoint)) 
        {
            string errorMessage = ControllerStringConstants.ErrorMessageThisEndpointCanAccess
                + rolesToAccessTheEdnpoint.Aggregate((s1, s2) => s1 + ", " + s2);
            return Unauthorized(errorMessage);
        }

        var model = _mapper.Map<TModel>(dto);
        if (!ModelState.IsValid)
        {
            return BadRequest(GetAllErrorsDuringValidation());
        }
        await _crudService.UpdateAsync(model, cancellationToken);
        return NoContent();
    }
}
