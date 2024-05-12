using WebApiForHikka.Dtos.MyOwnValidationAttribute;

namespace WebApiForHikka.Dtos.Shared;
public class CreateDtoWithSeoAddition
{
    [SeoAdditionValidation]
    public required Guid SeoAdditionId { get; set; }
}
