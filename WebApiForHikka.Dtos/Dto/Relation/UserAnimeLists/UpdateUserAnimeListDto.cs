using System.ComponentModel.DataAnnotations;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Relation.UserAnimeLists;

[MetadataType(typeof(UserAnimeList))]
[ExportTsInterface]
public class UpdateUserAnimeListDto : ModelDto
{
    [EntityValidation<User>] public required Guid UserId { get; set; }

    [EntityValidation<Anime>] public required Guid AnimeId { get; set; }


    public required bool IsFavorite { get; set; }
}