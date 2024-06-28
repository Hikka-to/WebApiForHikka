﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.EfPersistence.Data;

public class HikkaDbContext(DbContextOptions<HikkaDbContext> options) : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<SeoAddition> SeoAdditions { get; set; }
    public DbSet<Period> Periods { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Kind> Kinds { get; set; }
    public DbSet<Source> Sources { get; set; }
    public DbSet<Format> Formats { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<RestrictedRating> RestrictedRatings { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Studio> Studios { get; set; }
    public DbSet<Dub> Dubs { get; set; }
    public DbSet<Mediaplayer> Mediaplayers { get; set; }
    public DbSet<TagAnime> TagAnimes { get; set; }
    public DbSet<CountryAnime> CountryAnimes { get; set; }
    public DbSet<DubAnime> DubAnimes { get; set; }
    public DbSet<Anime> Animes { get; set; }
    public DbSet<AnimeBackdrop> AnimeBackdrops { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the self-referencing relationship
        modelBuilder.Entity<Tag>()
           .HasOne(t => t.ParentTag)
           .WithMany(t => t.Tags)
           .HasForeignKey("ParentId");

        modelBuilder.Entity<IdentityRole<Guid>>().HasData(
            new IdentityRole<Guid>
            {
                Id = Guid.Parse("2ae998d7-d8b1-4616-a0b3-60d29eca6c90"),
                Name = UserStringConstants.AdminRole,
                NormalizedName = UserStringConstants.AdminRole.ToUpper()
            },
            new IdentityRole<Guid>
            {
                Id = Guid.Parse("b1e76313-b130-44f8-ae76-6aff097064aa"),
                Name = UserStringConstants.UserRole,
                NormalizedName = UserStringConstants.UserRole.ToUpper()
            },
            new IdentityRole<Guid>
            {
                Id = Guid.Parse("5bf717f2-e546-417f-b33a-40eab3eafc96"),
                Name = UserStringConstants.BannedRole,
                NormalizedName = UserStringConstants.BannedRole.ToUpper()
            }
        );

        modelBuilder.Entity<AnimeBackdrop>().Navigation(e => e.Anime).AutoInclude();

        //User
        modelBuilder.Entity<User>().Navigation(e => e.Roles).AutoInclude();

        modelBuilder.Entity<User>()
            .HasMany(e => e.Roles)
            .WithMany()
            .UsingEntity<IdentityUserRole<Guid>>();

        //Anime
        modelBuilder.Entity<Anime>()
            .HasMany(e => e.Tags)
            .WithMany(e => e.Animes)
            .UsingEntity<TagAnime>(
                l => l.HasOne<Tag>().WithMany(e => e.TagAnimes).OnDelete(DeleteBehavior.Cascade),
                r => r.HasOne<Anime>().WithMany(e => e.TagAnimes).OnDelete(DeleteBehavior.Cascade)
            );

        modelBuilder.Entity<Anime>()
            .HasMany(e => e.Countries)
            .WithMany(e => e.Animes)
            .UsingEntity<CountryAnime>(
                l => l.HasOne<Country>().WithMany(e => e.CountryAnimes).OnDelete(DeleteBehavior.Cascade),
                r => r.HasOne<Anime>().WithMany(e => e.CountryAnimes).OnDelete(DeleteBehavior.Cascade)
            );

        modelBuilder.Entity<Anime>()
            .HasMany(e => e.Dubs)
            .WithMany(e => e.Animes)
            .UsingEntity<DubAnime>(
                l => l.HasOne<Dub>().WithMany(e => e.DubAnimes).OnDelete(DeleteBehavior.Cascade),
                r => r.HasOne<Anime>().WithMany(e => e.DubAnimes).OnDelete(DeleteBehavior.Cascade)
            );

        modelBuilder.Entity<Anime>().Navigation(e => e.Tags).AutoInclude();

        modelBuilder.Entity<Anime>().Navigation(e => e.Countries).AutoInclude();

        modelBuilder.Entity<Anime>().Navigation(e => e.Dubs).AutoInclude();

        modelBuilder.Entity<Anime>().Navigation(e => e.Status).AutoInclude();

        modelBuilder.Entity<Anime>().Navigation(e => e.Source).AutoInclude();

        modelBuilder.Entity<Anime>().Navigation(e => e.Kind).AutoInclude();

        modelBuilder.Entity<Anime>().Navigation(e => e.RestrictedRating).AutoInclude();

        modelBuilder.Entity<Anime>().Navigation(e => e.Period).AutoInclude();

        // SeoAddition auto include
        foreach (var seoAddition in modelBuilder.Model.GetEntityTypes().Where(e => typeof(ModelWithSeoAddition).IsAssignableFrom(e.ClrType)))
        {
            modelBuilder.Entity(seoAddition.ClrType).Navigation(nameof(ModelWithSeoAddition.SeoAddition)).AutoInclude();
        }
    }
}