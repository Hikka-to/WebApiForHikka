using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.WithoutSeoAddition.CommentReports;

namespace WebApiForHikka.Domain.Models.WithoutSeoAddition;

public class CommentReport : Model
{
    public virtual required Comment Comment { get; set; }
    public virtual required User User { get; set; }
    public virtual required CommentReportType CommentReportType { get; set; }

    [StringLength(CommentReportNumberConstants.BodyLength)]
    public string? Body { get; set; }
}