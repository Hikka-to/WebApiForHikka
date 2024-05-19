﻿using WebApiForHikka.Dtos.MyOwnValidationAttribute;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Tags;

public class CreateTagDto : CreateDtoWithSeoAddition
{
    public required string Name { get; set; }

    public required string EngName { get; set; }

    public required List<String> Alises { get; set; }

    public required bool IsGenre { get; set; }

    [TagValidation]
    public Guid? ParentTagId { get; set; }


}
