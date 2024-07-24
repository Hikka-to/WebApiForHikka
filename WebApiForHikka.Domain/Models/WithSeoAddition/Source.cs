using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Shared;

namespace WebApiForHikka.Domain.Models.WithSeoAddition;

public class Source : ModelWithSeoAddition
{
    [StringLength(SharedNumberConstatnts.NameLength)]
    public required string Name { get; set; }
}