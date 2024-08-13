using System.ComponentModel.DataAnnotations;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;

namespace WebApiForHikka.Dtos.Dto.Relation.UserAnimeLists;

[MetadataType(typeof(UserAnimeList))]
[ExportTsInterface]
public class CreateUserAnimeListDto
{
    [EntityValidation<UserAnimeListType>] public required Guid UserAnimeListTypeId { get; set; }

    public required bool IsFavorite { get; set; }
}