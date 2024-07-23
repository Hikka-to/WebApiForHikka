using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.LanguageMediaplayers;

[ModelMetadataType(typeof(LanguageMediaplayer))]
[ExportTsInterface]
public class UpdateLanguageMediaplayerDto : UpdateDtoWithSeoAddition
{
    [EntityValidation<LanguageMediaplayer>]
    public required Guid MediaplayerId { get; set; }

    [EntityValidation<Language>] public required Guid LanguageId { get; set; }

    [EntityValidation<Episode>] public required Guid EpisodeId { get; set; }

    [EntityValidation<Format>] public required Guid FormatId { get; set; }

    public required string Url { get; set; }

    public string? FileId { get; set; }

    public uint? StartIntro { get; set; }

    public uint? EndIntro { get; set; } = null;

    public uint? StartEnding { get; set; } = null;

    public uint? EndEnding { get; set; } = null;
}