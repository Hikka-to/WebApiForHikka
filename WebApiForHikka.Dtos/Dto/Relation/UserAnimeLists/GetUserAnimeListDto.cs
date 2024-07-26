using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Dto.Users;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.UserAnimeListTypes;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Animes;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Relation.UserAnimeLists;

[ExportTsInterface]
public class GetUserAnimeListDto : ModelDto
{
    public required GetUserAnimeListTypeDto UserAnimeListType { get; set; }

    public required GetUserDto User { get; set; }

    public required GetAnimeDto Anime { get; set; }

    public required bool IsFavorite { get; set; }
}