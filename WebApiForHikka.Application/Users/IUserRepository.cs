
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.Users;
public interface IUserRepository : ICrudRepository<User>
{
    public Task<User?> AuthenticateUserAsync(string email, string password, CancellationToken cancellationToken);

    public Task<bool> CheckIfUserWithTheEmailIsAlreadyExistAsync(string email, CancellationToken cancellationToken);
    public bool CheckIfUserWithTheEmailIsAlreadyExist(string email);

}
