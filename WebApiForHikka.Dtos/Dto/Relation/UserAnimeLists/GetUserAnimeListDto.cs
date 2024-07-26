using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.Users;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Animes;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Relation.UserAnimeLists;

[ExportTsInterface]
public class GetUserAnimeListDto : ModelDto
{
    public required UserAnimeListType UserAnimeListType { get; set; }

    public required GetUserDto User { get; set; }

    public required GetAnimeDto Anime { get; set; }

    public required bool IsFavorite { get; set; }
}