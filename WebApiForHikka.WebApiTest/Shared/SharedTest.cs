
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.WebApiTest.Repository.Users.FakeDataCreaters;

namespace WebApiForHikka.Test.Shared;
public class SharedTest
{
    protected CancellationToken _cancellationToken => new();

    protected HikkaDbContext GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<HikkaDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var databaseContext = new HikkaDbContext(options);
        databaseContext.Database.EnsureCreated();

        return databaseContext;
    }
}