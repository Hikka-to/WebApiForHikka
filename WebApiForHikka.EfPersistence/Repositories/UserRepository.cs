using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories;
public class UserRepository(
        HikkaDbContext dbContext,
        UserManager<User> userManager
    ) : CrudRepository<User>(dbContext), IUserRepository
{
    public async Task<User?> AuthenticateUserAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user != null && await userManager.CheckPasswordAsync(user, password))
        {
            return user;
        }
        return null;
    }

    public async Task<User?> AuthenticateUserWithAdminRoleAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user != null && await userManager.CheckPasswordAsync(user, password) && await userManager.IsInRoleAsync(user, UserStringConstants.AdminRole))
        {
            return user;
        }
        return null;
    }

    public override async Task<Guid> AddAsync(User model, CancellationToken cancellationToken)
    {
        var result = await userManager.CreateAsync(model, model.PasswordHash!);
        if (!result.Succeeded)
        {
            // !!!!!!!! Improve error handling
            throw new AggregateException(result.Errors.Select(e => new Exception(e.Description)));
        }

        return model.Id;
    }
    public async Task<bool> CheckIfUserWithTheEmailIsAlreadyExistAsync(string email, CancellationToken cancellationToken)
    {
        var user = await DbContext.Set<User>().FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        if (user == null)
        {
            return false;
        }
        return true;
    }
    public bool CheckIfUserWithTheEmailIsAlreadyExist(string email)
    {
        var user = DbContext.Set<User>().FirstOrDefault(u => u.Email == email);
        if (user == null)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> CheckIfUserWithTheUserNameIsAlreadyExistAsync(string username, CancellationToken cancellationToken)
    {
        return await DbContext.Set<User>().AnyAsync(u => u.UserName == username, cancellationToken);
    }

    public bool CheckIfUserWithTheUserNameIsAlreadyExist(string username)
    {
        return DbContext.Set<User>().Any(u => u.UserName == username);
    }
}
