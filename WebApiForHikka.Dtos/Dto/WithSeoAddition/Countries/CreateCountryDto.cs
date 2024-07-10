using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Countries;

[ModelMetadataType(typeof(Country))]
[ExportTsInterface(OutputDir = "./../admin-panel-hikka/models/Dto/WithSeoAddition/Countries")]
public class CreateCountryDto : CreateDtoWithSeoAddition
{
    public required string Name { get; set; }

    public required string Icon { get; set; }
}