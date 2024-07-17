using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Shared.Attributes;

namespace WebApiForHikka.Dtos.MyOwnValidationAttribute.EntityValidationAttributes;

public class AnimeValidationAttribute : EntityValidationAttribute<Anime, IAnimeService>;