using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Languages;


[ModelMetadataType(typeof(Language))]
[ExportTsInterface]
public class CreateLanguageDto : CreateDtoWithSeoAddition
{
    public required string Name { get; set; }
    public required string Locale { get; set; }
    public required string Icon { get; set; }

}