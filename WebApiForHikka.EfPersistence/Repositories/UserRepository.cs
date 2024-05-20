using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.SharedFunction.HashFunction;

namespace WebApiForHikka.EfPersistence.Repositories;
public class UserRepository
    (HikkaDbContext dbContext, IHashFunctions hashFunctions)
    : CrudRepository<User>(dbContext),
    IUserRepository
{
    private readonly IHashFunctions _hashFunctions = hashFunctions;

    public async Task<User?> AuthenticateUserAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await DbContext.Set<User>().FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        if (user != null && _hashFunctions.VerifyPassword(password, user.Password))
        {
            return user;
        }
        return null;
    }

    public async Task<User?> AuthenticateUserWithAdminRoleAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await DbContext.Set<User>().FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        if (user != null && _hashFunctions.VerifyPassword(password, user.Password) && user.Role == UserStringConstants.AdminRole)
        {
            return user;
        }
        return null;
    }

    public new async Task<Guid> AddAsync(User model, CancellationToken cancellationToken)
    {
        var user = model.Clone();
        user.Password = _hashFunctions.HashPassword(user.Password);
        await DbContext.Set<User>().AddAsync(user, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
        return user.Id;
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
            UserStringConstants.EmailName => query.Where(m => m.Email.Contains(filter, StringComparison.OrdinalIgnoreCase)),
            UserStringConstants.RoleName => query.Where(m => m.Role.Contains(filter, StringComparison.OrdinalIgnoreCase)),
            SharedStringConstants.IdName => query.Where(m => m.Id.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase)),
            _ => query.Where(m => m.Id.ToString().Contains(filter)),
        };
    }

    protected override IQueryable<User> Sort(IQueryable<User> query, string orderBy, bool isAscending)
    {
        return orderBy switch
        {
            UserStringConstants.EmailName => isAscending ? query.OrderBy(m => m.Email) : query.OrderByDescending(m => m.Email),
            UserStringConstants.RoleName => isAscending ? query.OrderBy(m => m.Role) : query.OrderByDescending(m => m.Role),
            SharedStringConstants.IdName => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id),
            _ => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id)
        };
    }

    protected override void Update(User model, User entity)
    {
        entity.Email = model.Email;
        entity.Role = model.Role;
        entity.Password = model.Password;
    }
}