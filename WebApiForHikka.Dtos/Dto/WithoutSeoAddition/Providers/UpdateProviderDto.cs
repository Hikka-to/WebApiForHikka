using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Providers;

public class UpdateProviderDto : ModelDto
{
    [EntityValidation<Anime>] public required Guid AnimeId { get; set; }

    public required string LogoPath { get; set; }

    public required string Name { get; set; }

    public required int Priority { get; set; }
}