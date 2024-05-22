
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.WebApiTest.Repository.Users.FakeDataCreaters;

namespace WebApiForHikka.Test.Shared;
public class SharedTest
{
    public CancellationToken CancellationToken => new();

    public async Task<HikkaDbContext> GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<HikkaDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var databaseContext = new HikkaDbContext(options);
        databaseContext.Database.EnsureCreated();
        await CreateUserFakeData.CreateUsersWithRoleAsync(databaseContext, UserStringConstants.UserRole, 10);
        await CreateUserFakeData.CreateUsersWithRoleAsync(databaseContext, UserStringConstants.AdminRole, 10);

        return databaseContext;
    }
}