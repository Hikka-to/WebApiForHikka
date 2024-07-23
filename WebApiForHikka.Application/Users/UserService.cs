using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.SharedFunction.Helpers.FileHelper;

namespace WebApiForHikka.Application.Users;

public class UserService(IUserRepository repository, IFileHelper fileHelper)
    : CrudService<User, IUserRepository>(repository), IUserService
{
    public override async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _repository.GetAsync(id, cancellationToken);

        if (user.BackdropPath != null)
            fileHelper.DeleteFile(user.BackdropPath);

        if (user.AvatarPath != null)
            fileHelper.DeleteFile(user.AvatarPath);

        await _repository.DeleteAsync(id, cancellationToken);
    }

    public async Task<User?> AuthenticateUserAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await _repository.AuthenticateUserAsync(email, password, cancellationToken);
        return user;
    }

    public async Task<Guid?> RegisterUserAsync(User user, CancellationToken cancellationToken)
    {
        return await _repository.AddAsync(user, cancellationToken);
    }

    public async Task<bool> CheckIfUserWithTheEmailIsAlreadyExistAsync(string email,
        CancellationToken cancellationToken)
    {
        return await _repository.CheckIfUserWithTheEmailIsAlreadyExistAsync(email, cancellationToken);
    }

    public bool CheckIfUserWithTheEmailIsAlreadyExist(string email)
    {
        return _repository.CheckIfUserWithTheEmailIsAlreadyExist(email);
    }

    public async Task<User?> AuthenticateUserWithAdminRoleAsync(string email, string password,
        CancellationToken cancellationToken)
    {
        return await _repository.AuthenticateUserWithAdminRoleAsync(email, password, cancellationToken);
    }

    public async Task<bool> CheckIfUserWithTheUserNameIsAlreadyExistAsync(string username,
        CancellationToken cancellationToken)
    {
        return await _repository.CheckIfUserWithTheUserNameIsAlreadyExistAsync(username, cancellationToken);
    }

    public bool CheckIfUserWithTheUserNameIsAlreadyExist(string username)
    {
        return _repository.CheckIfUserWithTheUserNameIsAlreadyExist(username);
    }
}