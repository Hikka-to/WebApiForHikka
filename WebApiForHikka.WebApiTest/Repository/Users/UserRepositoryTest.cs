using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiForHikka.Constants.Users;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.WebApiTest.Repository.Users.FakeDataCreaters;
using Xunit;


namespace WebApiForHikka.WebApiTest.Repository.Users;
class UserRepositoryTest
{
    private async Task<HikkaDbContext> GetDatabaseContext()
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
