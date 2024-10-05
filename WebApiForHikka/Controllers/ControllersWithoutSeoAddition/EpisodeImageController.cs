using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.WithoutSeoAddition.EpisodeImages;
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
using WebApiForHikka.SharedFunction.Helpers.LinkFactory;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

public class EpisodeImageController(
    EpisodeImageService crudService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    IEpisodeService episodeService,
    IFileHelper fileHelper,
    IColorHelper colorHelper,
    ILinkFactory linkfactory
)
    : CrudController
    <GetEpisodeImageDto, UpdateEpisodeImageDto, CreateEpisodeImageDto, EpisodeImageService,
        EpisodeImage>(
        crudService, mapper, httpContextAccessor)
{
    [AllowAnonymous]
    [HttpGet("downloadFile/{imageName}")]
    public IActionResult GetImage([FromRoute] string imageName)
    {
        var file = fileHelper.GetFile(ControllerStringConstants.EpisodeImagePath, imageName);

        return File(file, ControllerStringConstants.JsonImageReturnType, imageName);
    }


    public override async Task<IActionResult> Create([FromForm] CreateEpisodeImageDto dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = Mapper.Map<EpisodeImage>(dto);

        model.Episode = (await episodeService.GetAsync(dto.EpisodeId, cancellationToken))!;

        model.Path =
            fileHelper.UploadFileImage(dto.Image, ControllerStringConstants.EpisodeImagePath);

        model.Colors = colorHelper.GetListOfColorsFromImage(dto.Image);

        var heightWidth = fileHelper.GetHeightAndWidthOfImage(dto.Image);

        model.Width = heightWidth.width;
        model.Height = heightWidth.height;

        var createdId = await CrudService.CreateAsync(model, cancellationToken);

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

        var model = Mapper.Map<EpisodeImage>(dto);

        model.Episode = (await episodeService.GetAsync(dto.EpisodeId, cancellationToken))!;

        var path = await CrudService.GetImagePath(dto.Id);

        if (path == null)
        {
            model.Path =
                fileHelper.UploadFileImage(dto.Image, ControllerStringConstants.EpisodeImagePath);
        }
        else
        {
            fileHelper.OverrideFileImage(dto.Image, path);
            model.Path = path;
        }

        model.Colors = colorHelper.GetListOfColorsFromImage(dto.Image);

        var heightWidth = fileHelper.GetHeightAndWidthOfImage(dto.Image);

        model.Width = heightWidth.width;
        model.Height = heightWidth.height;

        await CrudService.UpdateAsync(model, cancellationToken);

        return NoContent();
    }

    [AllowAnonymous]
    public override async Task<IActionResult> GetAll(FilterPaginationDto paginationDto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());

        CkeckIfColumnsAreInModel(paginationDto, errorEndPoint);
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var filterPagination = Mapper.Map<FilterPagination>(paginationDto);

        var paginationCollection =
            await CrudService.GetAllAsync(filterPagination, cancellationToken);

        var models = Mapper.Map<List<GetEpisodeImageDto>>(paginationCollection.Models);

        foreach (var item in models)
            item.ImageUrl =
                linkfactory.GetLinkForDownloadImage(Request, "downloadFile", "GetAll",
                    item.ImageUrl);


        return Ok(
            new ReturnPageDto<GetEpisodeImageDto>
            {
                HowManyPages =
                    (int)Math.Ceiling(
                        (double)paginationCollection.Total / filterPagination.PageSize),
                Models = models,
                Total = paginationCollection.Total,
            }
        );
    }


    [HttpDelete("{id:Guid}")]
    public override async Task<IActionResult> Delete([FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = await CrudService.GetAsync(id, cancellationToken);
        if (model is null)
            NoContent();

        await CrudService.DeleteAsync(model!.Id, cancellationToken);
        fileHelper.DeleteFile(model.Path);
        return NoContent();
    }

    [HttpGet("{id:Guid}")]
    public override async Task<IActionResult> Get([FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model =
            Mapper.Map<GetEpisodeImageDto>(await CrudService.GetAsync(id, cancellationToken));

        if (model is null)
            return NotFound();


        model.ImageUrl =
            linkfactory.GetLinkForDownloadImage(Request, "downloadFile", "Get", model.ImageUrl);


        return Ok(model);
    }
}