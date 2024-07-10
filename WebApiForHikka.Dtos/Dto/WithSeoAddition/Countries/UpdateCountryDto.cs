using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Countries;

[ModelMetadataType(typeof(Country))]
[ExportTsInterface]
public class UpdateCountryDto : UpdateDtoWithSeoAddition
{
    public required string Name { get; set; }

    public required string Icon { get; set; }
}