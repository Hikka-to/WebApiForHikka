using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Application.WithoutSeoAddition.CommentReports;
using WebApiForHikka.Application.WithoutSeoAddition.CommentReportTypes;
using WebApiForHikka.Application.WithoutSeoAddition.Comments;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.CommentReports;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedFunction.Helpers.FileHelper;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithoutSeoAddition;

public class CommentReportControllerTest : CrudControllerBaseTest<
    CommentReportController,
    CommentReportService,
    CommentReport,
    ICommentReportRepository,
    UpdateCommentReportDto,
    CreateCommentReportDto,
    GetCommentReportDto,
    ReturnPageDto<GetCommentReportDto>
>
{
    protected override AllServicesInController GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var repository = new CommentReportRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        alternativeServices.AddSingleton(dbContext);
        alternativeServices.AddSingleton(userManager);
        alternativeServices.AddSingleton<IFileHelper, FileHelper>();
        alternativeServices.AddSingleton<IUserRepository, UserRepository>();
        alternativeServices.AddSingleton<IUserService, UserService>();
        alternativeServices.AddSingleton<ICommentRepository, CommentRepository>();
        alternativeServices.AddSingleton<ICommentService, CommentService>();
        alternativeServices.AddSingleton<ICommentReportTypeRepository, CommentReportTypeRepository>();
        alternativeServices.AddSingleton<ICommentReportTypeService, CommentReportTypeService>();

        return new AllServicesInController(new CommentReportService(repository), userManager, roleManager);
    }

    protected override async Task<CommentReportController> GetController(
        AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        return new CommentReportController(
            allServicesInController.CrudService,
            Mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager),
            alternativeServices.GetRequiredService<ICommentService>(),
            alternativeServices.GetRequiredService<IUserService>(),
            alternativeServices.GetRequiredService<ICommentReportTypeService>()
        );
    }

    protected override void MutationBeforeDtoCreation(CreateCommentReportDto createDto,
        AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        var user = SampleUser;
        var comment = GetCommentModels.GetSample();
        var commentReportType = GetCommentReportTypeModels.GetSample();

        var commentService = alternativeServices.GetRequiredService<ICommentService>();
        var commentReportTypeService = alternativeServices.GetRequiredService<ICommentReportTypeService>();

        commentService.CreateAsync(comment, default).Wait();
        commentReportTypeService.CreateAsync(commentReportType, default).Wait();

        createDto.UserId = user.Id;
        createDto.CommentId = comment.Id;
        createDto.CommentReportTypeId = commentReportType.Id;
    }

    protected override void MutationBeforeDtoUpdate(UpdateCommentReportDto updateDto,
        AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var user = SampleUser;
        var comment = GetCommentModels.GetSample();
        var commentReportType = GetCommentReportTypeModels.GetSample();

        var commentService = alternativeServices.GetRequiredService<ICommentService>();
        var commentReportTypeService = alternativeServices.GetRequiredService<ICommentReportTypeService>();

        commentService.CreateAsync(comment, default).Wait();
        commentReportTypeService.CreateAsync(commentReportType, default).Wait();

        updateDto.UserId = user.Id;
        updateDto.CommentId = comment.Id;
        updateDto.CommentReportTypeId = commentReportType.Id;
    }

    protected override CreateCommentReportDto GetCreateDtoSample()
    {
        return GetCommentReportModels.GetCreateDtoSample();
    }

    protected override GetCommentReportDto GetGetDtoSample()
    {
        return GetCommentReportModels.GetGetDtoSample();
    }

    protected override UpdateCommentReportDto GetUpdateDtoSample()
    {
        return GetCommentReportModels.GetUpdateDtoSample();
    }

    protected override CommentReport GetModelSample()
    {
        return GetCommentReportModels.GetSample();
    }
}