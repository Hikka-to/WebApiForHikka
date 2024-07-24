using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.CommentReports;

public interface ICommentReportRepository : ICrudRepository<CommentReport>;