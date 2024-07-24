using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.WithoutSeoAddition.EmojiGroups;

namespace WebApiForHikka.Domain.Models.WithoutSeoAddition;

public class EmojiGroup : Model
{
    [StringLength(EmojiGroupNumberConstants.NameLength)]
    public required string Name { get; set; }


    [StringLength(EmojiGroupNumberConstants.SlugLength)]
    public required string Slug { get; set; }
}