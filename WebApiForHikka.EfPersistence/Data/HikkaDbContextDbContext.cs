using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.EfPersistence.Data;

public class HikkaDbContext : DbContext
{

    public DbSet<User> Users { get; set; }
    public DbSet<SeoAddition> SeoAdditions { get; set; }
    public DbSet<Period> Periods { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Kind> Kinds { get; set; }
    public DbSet<Source> Sources { get; set; }
    public DbSet<RestrictedRating> RestrictedRatings { get; set; }

    public HikkaDbContext(DbContextOptions<HikkaDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Period>().Navigation(e => e.SeoAddition).AutoInclude();

        modelBuilder.Entity<Status>().Navigation(e => e.SeoAddition).AutoInclude();

        modelBuilder.Entity<Kind>().Navigation(e => e.SeoAddition).AutoInclude();

        modelBuilder.Entity<Source>().Navigation(e => e.SeoAddition).AutoInclude();

        modelBuilder.Entity<RestrictedRating>().Navigation(e => e.SeoAddition).AutoInclude();

    }
}
