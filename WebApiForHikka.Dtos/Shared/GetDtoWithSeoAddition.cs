using WebApiForHikka.Dtos.Dto.SeoAddition;

namespace WebApiForHikka.Dtos.Shared;

public class GetDtoWithSeoAddition : ModelDto
{
    public required GetSeoAdditionDto SeoAddition { get; set; }
}