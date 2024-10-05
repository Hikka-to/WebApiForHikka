using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Application.WithoutSeoAddition.UserSettings;
using WebApiForHikka.Constants.Controllers;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.SharedDtos;
using WebApiForHikka.Dtos.Dto.Users;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.SharedFunction.Helpers.FileHelper;
using WebApiForHikka.SharedFunction.Helpers.LinkFactory;
using WebApiForHikka.SharedFunction.JwtTokenFactories;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers;

[Authorize(ControllerStringConstants.CanAccessOnlyAdmin)]
public class UserController(
    IUserService userService,
    IJwtTokenFactory jwtTokenFactory,
    IConfiguration configuration,
    RoleManager<IdentityRole<Guid>> roleManager,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    IUserSettingService userSettingService,
    IFileHelper fileHelper,
    ILinkFactory linkFactory
)
    : MyBaseController(mapper, httpContextAccessor),
        ICrudController<UpdateUserDto, UserRegistrationDto>
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IJwtTokenFactory _jwtTokenFactory = jwtTokenFactory;
    private readonly IUserService _userService = userService;

    [AllowAnonymous]
    [HttpPost("Registration")]
    [SwaggerResponse(StatusCodes.Status200OK, "User registered",
        typeof(RegistratedResponseUserDto))]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Model Validation Error",
        typeof(IDictionary<string, IEnumerable<string>>))]
    public async Task<IActionResult> Create([FromBody] UserRegistrationDto model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return UnprocessableEntity(GetAllErrorsDuringValidation());

        var role = await roleManager.FindByNameAsync(model.Role);

        var user = GetUserModels.GetSample();
        user.UserName = model.UserName;
        user.Email = model.Email;
        user.PasswordHash = model.Password;
        user.Roles = [role!];

        var id = await _userService.RegisterUserAsync(user, cancellationToken);

        if (id == null) return NotFound(UserStringConstants.MessageUserIsntRegistrated);

        var tokenString = await _jwtTokenFactory.GetJwtTokenAsync(user, _configuration);

        return Ok(new RegistratedResponseUserDto
            {
                Message = UserStringConstants.MessageUserRegistrated,
                JwtToken = tokenString!,
                Id = (Guid)id
            }
        );
    }


    [HttpPost("GetAll")]
    [SwaggerResponse(StatusCodes.Status200OK, "Return all users", typeof(ReturnUserPageDto))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Model Validation Error",
        typeof(IDictionary<string, IEnumerable<string>>))]
    public async Task<IActionResult> GetAll([FromBody] FilterPaginationDto paginationDto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var filterPagination = Mapper.Map<FilterPagination>(paginationDto);

        var paginationCollection =
            await _userService.GetAllAsync(filterPagination, cancellationToken);

        var users = Mapper.Map<List<GetUserDto>>(paginationCollection.Models);

        foreach (var item in users)
        {
            if (item.BackdropUrl != null)
                item.BackdropUrl =
                    linkFactory.GetLinkForDownloadImage(Request, "downloadBackdrop", "GetAll",
                        item.BackdropUrl);

            if (item.AvatarUrl != null)
                item.AvatarUrl =
                    linkFactory.GetLinkForDownloadImage(Request, "downloadAvatar", "GetAll",
                        item.AvatarUrl);
        }

        return Ok(
            new ReturnUserPageDto
            {
                HowManyPages =
                    (int)Math.Ceiling(
                        (double)paginationCollection.Total / filterPagination.PageSize),
                Models = users,
                Total = paginationCollection.Total,
            }
        );
    }

    [HttpGet("{id:Guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Return user by id", typeof(GetUserDto))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Model Validation Error",
        typeof(IDictionary<string, IEnumerable<string>>))]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var user = Mapper.Map<GetUserDto>(await _userService.GetAsync(id, cancellationToken));

        if (user is null)
            return NotFound();

        if (user.BackdropUrl != null)
            user.BackdropUrl =
                linkFactory.GetLinkForDownloadImage(Request, "downloadBackdrop", "Get",
                    user.BackdropUrl);

        if (user.AvatarUrl != null)
            user.AvatarUrl =
                linkFactory.GetLinkForDownloadImage(Request, "downloadAvatar", "Get",
                    user.AvatarUrl);

        return Ok(user);
    }

    [HttpPut("Update")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "User updated")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found", typeof(string))]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Model Validation Error",
        typeof(IDictionary<string, IEnumerable<string>>))]
    public async Task<IActionResult> Put([FromForm] UpdateUserDto dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var user = Mapper.Map<User>(dto);

        var getUser = await _userService.GetAsync(user.Id, cancellationToken);
        if (getUser == null) return NotFound($"user with {user.Id} doesn't exist");

        if (getUser.BackdropPath != null && dto.BackdropImage != null)
        {
            fileHelper.OverrideFileImage(dto.BackdropImage, getUser.BackdropPath);
            user.BackdropPath = getUser.BackdropPath;
        }
        else if (dto.BackdropImage != null)
        {
            user.BackdropPath =
                fileHelper.UploadFileImage(dto.BackdropImage,
                    ControllerStringConstants.AnimeBackdropPath);
        }

        if (getUser.AvatarPath != null && dto.AvatarImage != null)
        {
            fileHelper.OverrideFileImage(dto.AvatarImage, getUser.AvatarPath);
            user.AvatarPath = getUser.AvatarPath;
        }
        else if (dto.AvatarImage != null)
        {
            user.AvatarPath = fileHelper.UploadFileImage(dto.AvatarImage,
                ControllerStringConstants.AnimeBackdropPath);
        }

        user.UserSetting.Id = getUser.UserSetting.Id;
        await userSettingService.UpdateAsync(user.UserSetting, cancellationToken);
        user.UserSetting = getUser.UserSetting;

        await _userService.UpdateAsync(user, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:Guid}")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "User deleted")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Model Validation Error",
        typeof(IDictionary<string, IEnumerable<string>>))]
    public async Task<IActionResult> Delete([FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        await _userService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }


    [AllowAnonymous]
    [HttpGet("downloadAvatar/{avatarImageName}")]
    public IActionResult GetAvatarImage([FromRoute] string avatarImageName)
    {
        var file =
            fileHelper.GetFile(ControllerStringConstants.AvatarBackdropPath, avatarImageName);
        return File(file, ControllerStringConstants.JsonImageReturnType, avatarImageName);
    }

    [AllowAnonymous]
    [HttpGet("downloadBackdrop/{backdropImageName}")]
    public IActionResult GetBackdropImage([FromRoute] string backdropImageName)
    {
        var file =
            fileHelper.GetFile(ControllerStringConstants.UserBackdropPath, backdropImageName);
        return File(file, ControllerStringConstants.JsonImageReturnType, backdropImageName);
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    [SwaggerResponse(StatusCodes.Status200OK, "User logged in", typeof(LoginResponseUserDto))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Model Validation Error",
        typeof(IDictionary<string, IEnumerable<string>>))]
    public async Task<IActionResult> Login([FromBody] UserLoginDto model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return UnprocessableEntity(GetAllErrorsDuringValidation());

        var user =
            await _userService.AuthenticateUserAsync(model.Email, model.Password,
                cancellationToken);
        if (user == null) return Unauthorized();

        var tokenString = await _jwtTokenFactory.GetJwtTokenAsync(user, _configuration);

        return Ok(new LoginResponseUserDto { Token = tokenString! });
    }

    [AllowAnonymous]
    [HttpPost("LoginAdmin")]
    [SwaggerResponse(StatusCodes.Status200OK, "User logged in", typeof(LoginResponseUserDto))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Model Validation Error",
        typeof(IDictionary<string, IEnumerable<string>>))]
    public async Task<IActionResult> LoginAdmin([FromBody] UserLoginDto model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return UnprocessableEntity(GetAllErrorsDuringValidation());

        var user =
            await _userService.AuthenticateUserWithAdminRoleAsync(model.Email, model.Password,
                cancellationToken);
        if (user == null) return Unauthorized();

        var tokenString = await _jwtTokenFactory.GetJwtTokenAsync(user, _configuration);

        return Ok(new LoginResponseUserDto { Token = tokenString! });
    }
}