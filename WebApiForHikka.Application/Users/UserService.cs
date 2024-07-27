using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.SharedFunction.Helpers.FileHelper;

namespace WebApiForHikka.Application.Users;

public class UserService(IUserRepository repository, IFileHelper fileHelper)
    : CrudService<User, IUserRepository>(repository), IUserService
{
    public override async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await Repository.GetAsync(id, cancellationToken);

        if (user?.BackdropPath != null)
            fileHelper.DeleteFile(user.BackdropPath);

        if (user?.AvatarPath != null)
            fileHelper.DeleteFile(user.AvatarPath);

        await Repository.DeleteAsync(id, cancellationToken);
    }

    public async Task<User?> AuthenticateUserAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await Repository.AuthenticateUserAsync(email, password, cancellationToken);
        return user;
    }

    public async Task<Guid?> RegisterUserAsync(User user, CancellationToken cancellationToken)
    {
        return await Repository.AddAsync(user, cancellationToken);
    }

    public async Task<bool> CheckIfUserWithTheEmailIsAlreadyExistAsync(string email,
        CancellationToken cancellationToken)
    {
        return await Repository.CheckIfUserWithTheEmailIsAlreadyExistAsync(email, cancellationToken);
    }

    public bool CheckIfUserWithTheEmailIsAlreadyExist(string email)
    {
        return Repository.CheckIfUserWithTheEmailIsAlreadyExist(email);
    }

    public async Task<User?> AuthenticateUserWithAdminRoleAsync(string email, string password,
        CancellationToken cancellationToken)
    {
        return await Repository.AuthenticateUserWithAdminRoleAsync(email, password, cancellationToken);
    }

    public async Task<bool> CheckIfUserWithTheUserNameIsAlreadyExistAsync(string username,
        CancellationToken cancellationToken)
    {
        return await Repository.CheckIfUserWithTheUserNameIsAlreadyExistAsync(username, cancellationToken);
    }

    public bool CheckIfUserWithTheUserNameIsAlreadyExist(string username)
    {
        return Repository.CheckIfUserWithTheUserNameIsAlreadyExist(username);
    }
}