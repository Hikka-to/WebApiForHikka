using WebApiForHikka.Dtos.Dto.SeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;

namespace WebApiForHikka.Dtos.Shared;
public class UpdateDtoWithSeoAddition : ModelDto
{

    public required UpdateSeoAdditionDto SeoAddition { get; set; }
}
