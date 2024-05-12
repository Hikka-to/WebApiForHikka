using WebApiForHikka.Dtos.MyOwnValidationAttribute;

namespace WebApiForHikka.Dtos.Shared;
public class UpdateDtoWithSeoAddition : ModelDto
{

    [SeoAdditionValidation]
    public required Guid SeoAdditionId { get; set; }
}
