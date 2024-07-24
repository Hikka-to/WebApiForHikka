using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Application.WithoutSeoAddition.CommentLikes;
using WebApiForHikka.Application.WithoutSeoAddition.Comments;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.CommentLikes;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedFunction.Helpers.FileHelper;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithoutSeoAddition;

public class CommentLikeControllerTest : CrudControllerBaseTest<
    CommentLikeController,
    CommentLikeService,
    CommentLike,
    ICommentLikeRepository,
    UpdateCommentLikeDto,
    CreateCommentLikeDto,
    GetCommentLikeDto,
    ReturnPageDto<GetCommentLikeDto>
>
{
    protected override AllServicesInController GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var repository = new CommentLikeRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        alternativeServices.AddSingleton(dbContext);
        alternativeServices.AddSingleton(userManager);
        alternativeServices.AddSingleton<IFileHelper, FileHelper>();
        alternativeServices.AddSingleton<IUserRepository, UserRepository>();
        alternativeServices.AddSingleton<IUserService, UserService>();
        alternativeServices.AddSingleton<ICommentRepository, CommentRepository>();
        alternativeServices.AddSingleton<ICommentService, CommentService>();

        return new AllServicesInController(new CommentLikeService(repository), userManager, roleManager);
    }

    protected override async Task<CommentLikeController> GetController(
        AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        return new CommentLikeController(
            allServicesInController.CrudService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager),
            alternativeServices.GetRequiredService<ICommentService>(),
            alternativeServices.GetRequiredService<IUserService>()
        );
    }

    protected override void MutationBeforeDtoCreation(CreateCommentLikeDto createDto,
        AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        var user = SampleUser;
        var comment = GetCommentModels.GetSample();

        var commentService = alternativeServices.GetRequiredService<ICommentService>();

        commentService.CreateAsync(comment, CancellationToken.None).Wait();

        createDto.UserId = user.Id;
        createDto.CommentId = comment.Id;
    }

    protected override void MutationBeforeDtoUpdate(UpdateCommentLikeDto updateDto,
        AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var user = SampleUser;
        var comment = GetCommentModels.GetSample();

        var commentService = alternativeServices.GetRequiredService<ICommentService>();

        commentService.CreateAsync(comment, CancellationToken.None).Wait();

        updateDto.UserId = user.Id;
        updateDto.CommentId = comment.Id;
    }

    protected override CreateCommentLikeDto GetCreateDtoSample()
    {
        return GetCommentLikeModels.GetCreateDtoSample();
    }

    protected override GetCommentLikeDto GetGetDtoSample()
    {
        return GetCommentLikeModels.GetGetDtoSample();
    }

    protected override UpdateCommentLikeDto GetUpdateDtoSample()
    {
        return GetCommentLikeModels.GetUpdateDtoSample();
    }

    protected override CommentLike GetModelSample()
    {
        return GetCommentLikeModels.GetSample();
    }
}