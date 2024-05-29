using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Shared;

namespace WebApiForHikka.Domain.Models;
public class Source : ModelWithSeoAddition
{
    [StringLength(SharedNumberConstatnts.NameLenght)]
    public required string Name;
}