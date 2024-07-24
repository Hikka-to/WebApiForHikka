using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.WithSeoAddition.Languages;

namespace WebApiForHikka.Domain.Models.WithSeoAddition;

public class Language : ModelWithSeoAddition
{
    [StringLength(LanguageNumberConstants.NameLength)]
    public required string Name { get; set; }

    [StringLength(LanguageNumberConstants.LocaleLength)]
    public required string Locale { get; set; }

    [StringLength(LanguageNumberConstants.IconLength)]
    public required string Icon { get; set; }
}