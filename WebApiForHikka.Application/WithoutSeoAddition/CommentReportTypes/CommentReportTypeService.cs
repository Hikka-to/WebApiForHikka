using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.CommentReportTypes;

public class CommentReportTypeService(ICommentReportTypeRepository repository)
    : CrudService<CommentReportType, ICommentReportTypeRepository>(repository), ICommentReportTypeService;