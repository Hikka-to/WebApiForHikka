using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.WithoutSeoAddition.CommentReportTypes;

namespace WebApiForHikka.Domain.Models.WithoutSeoAddition;

public class CommentReportType : Model
{
    [StringLength(CommentReportTypeNumberConstants.SlugLength)]
    public required string Slug { get; set; }
}