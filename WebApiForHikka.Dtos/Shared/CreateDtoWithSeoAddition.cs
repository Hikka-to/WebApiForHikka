using WebApiForHikka.Dtos.Dto.SeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;

namespace WebApiForHikka.Dtos.Shared;
public class CreateDtoWithSeoAddition
{
    public required CreateSeoAdditionDto SeoAddition { get; set; }
}
