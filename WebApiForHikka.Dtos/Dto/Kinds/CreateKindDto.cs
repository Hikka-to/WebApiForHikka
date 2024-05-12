using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Kinds;

public class CreateKindDto : CreateDtoWithSeoAddition
{
  public required string Slug { get; set; }

    [StringLength(KindNumberConstants.HintLenght)]
    public required string Hint { get; set; }

}
