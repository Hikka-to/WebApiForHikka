using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.CommentReports;

public class CommentReportService(ICommentReportRepository repository)
    : CrudService<CommentReport, ICommentReportRepository>(repository), ICommentReportService;