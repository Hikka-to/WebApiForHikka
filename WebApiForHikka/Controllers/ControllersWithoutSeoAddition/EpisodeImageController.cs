using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.WithoutSeoAddition.EpisodeImages;
using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Application.WithSeoAddition.Episodes;
using WebApiForHikka.Constants.Controllers;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.SharedDtos;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Episodes;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.SharedFunction.Helpers.ColorHelper;
using WebApiForHikka.SharedFunction.Helpers.FileHelper;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

public class EpisodeImageController(
    EpisodeImageService crudService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    IEpisodeService episodeService,
    IFileHelper _fileHelper,
    IColorHelper _colorHelper
)
 : CrudController
    <GetEpisodeImageDto, UpdateEpisodeImageDto, CreateEpisodeImageDto, EpisodeImageService, EpisodeImage>(crudService, mapper, httpContextAccessor)
{
    [AllowAnonymous]
    [HttpGet("dowloadFile/{imageName}")]
    public IActionResult GetImage([FromRoute] string imageName)
    {
        var file = _fileHelper.GetFile(ControllerStringConstants.EpisodeImagePath, imageName);

        return File(file, ControllerStringConstants.JsonImageReturnType, imageName);
    }


    public override async Task<IActionResult> Create([FromForm] CreateEpisodeImageDto dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = _mapper.Map<EpisodeImage>(dto);

        model.Episode = (await episodeService.GetAsync(dto.EpisodeId, cancellationToken))!;

        model.Path = _fileHelper.UploadFileImage(dto.Image, ControllerStringConstants.EpisodeImagePath);

        model.Colors = _colorHelper.GetListOfColorsFromImage(dto.Image);

        var heightWidth = _fileHelper.GetHeightAndWidthOfImage(dto.Image);

        model.Width = heightWidth.width;
        model.Height = heightWidth.height;

        var createdId = await CrudRelationService.CreateAsync(model, cancellationToken);

        return Ok(new CreateResponseDto { Id = createdId });
    }

    public override async Task<IActionResult> Put([FromForm] UpdateEpisodeImageDto dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequestForUpdateEndPoint(new ThingsToValidateForUpdate
        {
            UpdateDto = dto
        });
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = _mapper.Map<EpisodeImage>(dto);

        model.Episode = (await episodeService.GetAsync(dto.EpisodeId, cancellationToken))!;

        var path = await CrudRelationService.GetImagePath(dto.Id);

        if (path == null)
        {
            model.Path = _fileHelper.UploadFileImage(dto.Image, ControllerStringConstants.EpisodeImagePath);
        }
        else
        {
            _fileHelper.OverrideFileImage(dto.Image, path!);
            model.Path = path;
        }

        model.Colors = _colorHelper.GetListOfColorsFromImage(dto.Image);

        var heightWidth = _fileHelper.GetHeightAndWidthOfImage(dto.Image);

        model.Width = heightWidth.width;
        model.Height = heightWidth.height;

        await CrudRelationService.UpdateAsync(model, cancellationToken);

        return NoContent();
    }

    [AllowAnonymous]
    public override async Task<IActionResult> GetAll([FromQuery] FilterPaginationDto paginationDto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var filterPagination = _mapper.Map<FilterPagination>(paginationDto);

        var paginationCollection = await CrudRelationService.GetAllAsync(filterPagination, cancellationToken);

        var models = _mapper.Map<List<GetEpisodeImageDto>>(paginationCollection.Models);

        foreach (var item in models)
            item.ImageUrl = $"{Request.Scheme}://{Request.Host.Value}" +
                            Request.Path.Value.Replace("GetAll", string.Empty) + "dowloadFile/" +
                            item.ImageUrl.Split('\\').Last();


        return Ok(
            new ReturnPageDto<GetEpisodeImageDto>
            {
                HowManyPages = (int)Math.Ceiling((double)paginationCollection.Total / filterPagination.PageSize),
                Models = models
            }
        );
    }


    [HttpDelete("{id:Guid}")]
    public override async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = await CrudRelationService.GetAsync(id, cancellationToken);
        if (model is null)
            NoContent();

        await CrudRelationService.DeleteAsync(model!.Id, cancellationToken);
        _fileHelper.DeleteFile(model.Path);
        return NoContent();
    }

    [HttpGet("{id:Guid}")]
    public override async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = _mapper.Map<GetEpisodeImageDto>(await CrudRelationService.GetAsync(id, cancellationToken));

        if (model is null)
            return NotFound();

        model.ImageUrl = $"{Request.Scheme}://{Request.Host.Value}" +
                         Request.Path.Value.Substring(0, Request.Path.Value.IndexOf("Get")) + "dowloadFile/" +
                         model.ImageUrl.Split('\\').Last();


        return Ok(model);
    }
}
