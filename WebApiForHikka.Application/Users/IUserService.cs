
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.Users;
public interface IUserService : ICrudService<User>
{
    public Task<Guid?> RegisterUserAsync(User user, CancellationToken cancellationToken);
    public Task<User?> AuthenticateUserAsync(string email, string password, CancellationToken cancellationToken);

    public Task<User?> AuthenticateUserWithAdminRoleAsync(string email, string password, CancellationToken cancellationToken);
    public Task<bool> CheckIfUserWithTheEmailIsAlreadyExistAsync(string email, CancellationToken cancellationToken);
    public bool CheckIfUserWithTheEmailIsAlreadyExist(string email);
}
