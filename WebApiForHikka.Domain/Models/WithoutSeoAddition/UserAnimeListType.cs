using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Shared;

namespace WebApiForHikka.Domain.Models.WithoutSeoAddition;

public class UserAnimeListType : Model
{
    [StringLength(SharedNumberConstatnts.SlugLength)]
    public required string Slug { get; set; }
    
    [StringLength(SharedNumberConstatnts.NameLength)]
    public required string Name { get; set; }
}