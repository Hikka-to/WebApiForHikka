using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.EfPersistence.Data;

public class HikkaDbContext : DbContext
{

    public DbSet<User> Users { get; set; }
    public DbSet<SeoAddition> SeoAdditions { get; set; }
    public DbSet<Period> Periods { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Kind> Kinds { get; set; }
    public DbSet<Source> Sources { get; set; }
    public DbSet<Format> Formats { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<RestrictedRating> RestrictedRatings { get; set; }

    public HikkaDbContext(DbContextOptions<HikkaDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the self-referencing relationship
        modelBuilder.Entity<Tag>()
           .HasOne(t => t.ParentTag)
           .WithMany()
           .HasForeignKey("ParentId") // Assuming you meant to use "ParentId" instead of "parent_id"
           .IsRequired(false); // Make foreign key optional

        // Configure the collection of tags
        modelBuilder.Entity<Tag>()
           .HasMany(t => t.Tags)
           .WithOne() // No inverse property needed here
           .HasForeignKey("ParentId"); // Use the same foreign key for consistency


        modelBuilder.Entity<Tag>().Navigation(e => e.SeoAddition).AutoInclude();

        modelBuilder.Entity<Period>().Navigation(e => e.SeoAddition).AutoInclude();

        modelBuilder.Entity<Format>().Navigation(e => e.SeoAddition).AutoInclude();

        modelBuilder.Entity<Status>().Navigation(e => e.SeoAddition).AutoInclude();

        modelBuilder.Entity<Kind>().Navigation(e => e.SeoAddition).AutoInclude();

        modelBuilder.Entity<Source>().Navigation(e => e.SeoAddition).AutoInclude();

        modelBuilder.Entity<RestrictedRating>().Navigation(e => e.SeoAddition).AutoInclude();
      
    }
}
