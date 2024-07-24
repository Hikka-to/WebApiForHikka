using WebApiForHikka.Application.WithoutSeoAddition.CommentReportTypes;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;

public class CommentReportTypeRepository(HikkaDbContext dbContext)
    : CrudRepository<CommentReportType>(dbContext), ICommentReportTypeRepository;