﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeBackdrops;
using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Constants.Controllers;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.SharedDtos;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeBackdrops;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.SharedFunction.Helpers.ColorHelper;
using WebApiForHikka.SharedFunction.Helpers.FileHelper;
using WebApiForHikka.SharedFunction.Helpers.LinkFactory;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

public class AnimeBackdropController(
    IAnimeBackdropService crudService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    IAnimeService animeService,
    IFileHelper fileHelper,
    IColorHelper colorHelper,
    ILinkFactory linkFactory
)
    : CrudController<
        GetAnimeBackdropDto,
        UpdateAnimeBackdropDto,
        CreateAnimeBackdropDto,
        IAnimeBackdropService,
        AnimeBackdrop
    >(crudService, mapper, httpContextAccessor)
{
    [AllowAnonymous]
    [HttpGet("downloadFile/{imageName}")]
    public IActionResult GetImage([FromRoute] string imageName)
    {
        var file = fileHelper.GetFile(ControllerStringConstants.AnimeBackdropPath, imageName);

        return File(file, ControllerStringConstants.JsonImageReturnType, imageName);
    }


    public override async Task<IActionResult> Create([FromForm] CreateAnimeBackdropDto dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = Mapper.Map<AnimeBackdrop>(dto);

        model.Anime = (await animeService.GetAsync(dto.AnimeId, cancellationToken))!;

        model.Path =
            fileHelper.UploadFileImage(dto.Image, ControllerStringConstants.AnimeBackdropPath);

        model.Colors = colorHelper.GetListOfColorsFromImage(dto.Image);

        var heightWidth = fileHelper.GetHeightAndWidthOfImage(dto.Image);

        model.Width = heightWidth.width;
        model.Height = heightWidth.height;

        var createdId = await CrudService.CreateAsync(model, cancellationToken);

        return Ok(new CreateResponseDto { Id = createdId });
    }

    public override async Task<IActionResult> Put([FromForm] UpdateAnimeBackdropDto dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequestForUpdateEndPoint(new ThingsToValidateForUpdate
        {
            UpdateDto = dto
        });
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = Mapper.Map<AnimeBackdrop>(dto);

        model.Anime = (await animeService.GetAsync(dto.AnimeId, cancellationToken))!;

        var path = await CrudService.GetImagePathAsync(dto.Id);

        if (path == null)
        {
            model.Path =
                fileHelper.UploadFileImage(dto.Image, ControllerStringConstants.AnimeBackdropPath);
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

        var models = Mapper.Map<List<GetAnimeBackdropDto>>(paginationCollection.Models);

        foreach (var item in models)
            item.ImageUrl =
                linkFactory.GetLinkForDownloadImage(Request, "downloadImage", "GetAll",
                    item.ImageUrl);


        return Ok(
            new ReturnPageDto<GetAnimeBackdropDto>
            {
                HowManyPages =
                    (int)Math.Ceiling(
                        (double)paginationCollection.Total / filterPagination.PageSize),
                Models = models,
                Total = paginationCollection.Total
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
            Mapper.Map<GetAnimeBackdropDto>(await CrudService.GetAsync(id, cancellationToken));

        if (model is null)
            return NotFound();


        model.ImageUrl =
            linkFactory.GetLinkForDownloadImage(Request, "downloadImage", "Get", model.ImageUrl);


        return Ok(model);
    }
}