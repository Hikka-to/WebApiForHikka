using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Shared;

namespace WebApiForHikka.Domain.Models;

public class Format : ModelWithSeoAddition
{

    [StringLength(SharedNumberConstatnts.NameLength)]
    public required string Name { get; set; }
}