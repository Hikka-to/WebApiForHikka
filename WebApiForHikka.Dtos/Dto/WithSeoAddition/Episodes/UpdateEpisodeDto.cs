using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Episodes;

[ModelMetadataType(typeof(Episode))]
[ExportTsInterface]
public class UpdateEpisodeDto : UpdateDtoWithSeoAddition
{
    [EntityValidationAttribute<Anime>] public  Guid AnimeId { get; set; }
    public required string Name { get; set; }

    public required int Duration { get; set; }

    public required DateTime AirDate { get; set; }

    public bool IsFiller { get; set; }

}
