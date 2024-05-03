using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.EfPersistence.Data;

public class HikkaDbContext : DbContext
{

    public DbSet<User> Users { get; set; }

    public HikkaDbContext(DbContextOptions<HikkaDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
          }
}
