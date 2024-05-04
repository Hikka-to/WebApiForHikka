
using SushiRestaurant.EfPersistence.Repositories;
using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Constants.Users;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using System.Threading;
using WebApiForHikka.SharedFunction.HashFunction;

namespace WebApiForHikka.EfPersistence.Repositories;
public class UserRepository : CrudRepository<User>, IUserRepository
{
    private readonly IHashFunctions _hashFunctions;

    public UserRepository(HikkaDbContext dbContext, IHashFunctions hashFunctions) : base(dbContext)
    {
        _hashFunctions = hashFunctions;
    }

    public async Task<User?> AuthenticateUserAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await DbContext.Set<User>().FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        if (user != null && _hashFunctions.VerifyPassword(password, user.Password))
        {
            return user;
        }
        return null;
    }

    public new async Task<Guid> AddAsync(User model, CancellationToken cancellationToken)
    {
        model.Password = _hashFunctions.HashPassword(model.Password);
        await DbContext.Set<User>().AddAsync(model, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
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
            UserStringConstants.EmailName => query.Where(m => m.Email.Contains(filter)),
            UserStringConstants.RoleName => query.Where(m => m.Role.Contains(filter)),
            SharedStringConstants.IdName => query.Where(m => m.Id.ToString().Contains(filter)),
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
