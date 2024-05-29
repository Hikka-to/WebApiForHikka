using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Shared;

namespace WebApiForHikka.Domain.Models;
public class Status : ModelWithSeoAddition
{

    [StringLength(SharedNumberConstatnts.NameLenght)]
    public required string Name { get; set; }
}