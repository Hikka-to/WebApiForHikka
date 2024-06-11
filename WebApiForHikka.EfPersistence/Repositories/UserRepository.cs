using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.SharedFunction.HashFunction;

namespace WebApiForHikka.EfPersistence.Repositories;
public class UserRepository(
        HikkaDbContext dbContext,
        IHashFunctions hashFunctions,
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
            await userManager.AddToRoleAsync(model, model.Role);

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


    protected override IQueryable<User> Filter(IQueryable<User> query, string filterBy, string filter)
    {
        return filterBy switch
        {
            UserStringConstants.EmailName => query.Where(m => m.Email != null && m.Email.Contains(filter, StringComparison.OrdinalIgnoreCase)),
            UserStringConstants.RoleName => from user in query
                                            join userRole in DbContext.UserRoles on user.Id equals userRole.UserId
                                            join role in DbContext.Roles on userRole.RoleId equals role.Id
                                            where role.Name != null && role.Name.Contains(filter, StringComparison.OrdinalIgnoreCase)
                                            select user,
            SharedStringConstants.IdName => query.Where(m => m.Id.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase)),
            _ => query.Where(m => m.Id.ToString().Contains(filter)),
        };
    }

    protected override IQueryable<User> Sort(IQueryable<User> query, string orderBy, bool isAscending)
    {
        return orderBy switch
        {
            UserStringConstants.EmailName => isAscending ? query.OrderBy(m => m.Email) : query.OrderByDescending(m => m.Email),
            UserStringConstants.RoleName => isAscending
                ? (from user in query
                   join userRole in DbContext.UserRoles on user.Id equals userRole.UserId
                   join role in DbContext.Roles on userRole.RoleId equals role.Id
                   orderby role.Name ascending
                   select user)
                : (from user in query
                   join userRole in DbContext.UserRoles on user.Id equals userRole.UserId
                   join role in DbContext.Roles on userRole.RoleId equals role.Id
                   orderby role.Name descending
                   select user),
            SharedStringConstants.IdName => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id),
            _ => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id)
        };
    }

    protected override void Update(User model, User entity)
    {
        DbContext.Entry(entity).CurrentValues.SetValues(model);
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
