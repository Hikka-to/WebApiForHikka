using WebApiForHikka.Dtos.Dto.SeoAddition;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Statuses;
public class CreateStatusDto : CreateDtoWithSeoAddition
{
    public required string Name { get; set; }

}
