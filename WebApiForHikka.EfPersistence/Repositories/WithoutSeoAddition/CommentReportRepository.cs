using WebApiForHikka.Application.WithoutSeoAddition.CommentReports;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;

public class CommentReportRepository(HikkaDbContext context)
    : CrudRepository<CommentReport>(context), ICommentReportRepository;