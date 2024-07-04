﻿using WebApiForHikka.Application.WithoutSeoAddition.AnimeVideoKinds;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Shared.Attributes;

namespace WebApiForHikka.Dtos.MyOwnValidationAttribute.EntityValidationAttributes;

public class AnimeVideoKindValidationAttribute : EntityValidationAttribute<AnimeVideoKind, IAnimeVideoKindService>;
