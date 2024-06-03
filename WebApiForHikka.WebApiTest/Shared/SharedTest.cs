using Microsoft.EntityFrameworkCore;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.Test.Shared;
public class SharedTest
{
    protected CancellationToken CancellationToken => new();

    protected HikkaDbContext GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<HikkaDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).EnableSensitiveDataLogging().Options;
        var databaseContext = new HikkaDbContext(options);
        databaseContext.Database.EnsureCreated();

        return databaseContext;
    }
}