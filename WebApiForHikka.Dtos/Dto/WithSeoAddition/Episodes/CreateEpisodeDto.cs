using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Constants.Models.WithSeoAddition.Episodes;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Episodes;

[ModelMetadataType(typeof(Episode))]
[ExportTsInterface]
public class CreateEpisodeDto : CreateDtoWithSeoAddition
{
    [EntityValidationAttribute<Anime>] public  Guid AnimeId { get; set; }


    public required string Name { get; set; }

    public required int Duration { get; set; }

    public required DateTime AirDate { get; set; }

    public bool IsFiller { get; set; } = false;

}
