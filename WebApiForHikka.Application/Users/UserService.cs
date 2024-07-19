using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.Users;

public class UserService(IUserRepository repository) : CrudService<User, IUserRepository>(repository), IUserService
{
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