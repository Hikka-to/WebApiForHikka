using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.WithoutSeoAddition.Providers;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Domain.Models.WithoutSeoAddition;

public class Provider : Model
{
    public required virtual Anime Anime { get; set; }

    [StringLength(ProviderNumberConstants.LogoPathLength)]
    public required string LogoPath { get; set; }

    [StringLength(SharedNumberConstatnts.NameLength)]
    public required string Name { get; set; }

    public required int Priority { get; set; }
}