using Microsoft.EntityFrameworkCore;

namespace WebApiForHikka.EfPersistence.Data;

public class HikkaDbContext : DbContext
{
   

    public HikkaDbContext(DbContextOptions<HikkaDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
          }
}
