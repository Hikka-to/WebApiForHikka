using WebApiForHikka.Dtos.Dto.SeoAddition;

namespace WebApiForHikka.Dtos.Shared;
public class UpdateDtoWithSeoAddition : ModelDto
{
    public required UpdateSeoAdditionDto SeoAddition { get; set; }
}