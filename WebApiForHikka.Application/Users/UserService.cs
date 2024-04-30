
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.Users;
public class UserService : CrudService<User>, IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository repository) : base(repository)
    {
        _userRepository = repository; 
    }

    public async Task<User?> AuthenticateUserAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await _userRepository.AuthenticateUserAsync(email, password, cancellationToken);
        return user;
    }

    public async Task<Guid?> RegisterUserAsync(User user, CancellationToken cancellationToken)
    {
        return await _repository.AddAsync(user, cancellationToken);
    }
}
