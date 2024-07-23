using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.WithoutSeoAddition.RelatedType;

namespace WebApiForHikka.Domain.Models.WithoutSeoAddition;

public class RelatedType : Model
{
    [StringLength(RelatedTypeNumberConstatnts.NameLength)]
    public required string Name { get; set; }
}