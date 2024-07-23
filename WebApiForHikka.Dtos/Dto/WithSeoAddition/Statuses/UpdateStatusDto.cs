using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Statuses;

[ModelMetadataType(typeof(Status))]
[ExportTsInterface]
public class UpdateStatusDto : UpdateDtoWithSeoAddition
{
    public required string Name { get; set; }
}