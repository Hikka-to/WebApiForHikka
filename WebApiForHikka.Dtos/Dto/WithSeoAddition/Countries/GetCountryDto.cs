using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Countries;

public class GetCountryDto : GetDtoWithSeoAddition
{
    public required string Name { get; set; }
    public required string Icon { get; set; }
}