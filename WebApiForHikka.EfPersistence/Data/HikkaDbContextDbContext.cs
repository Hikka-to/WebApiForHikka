﻿using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
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
    public DbSet<Country> Countries { get; set; }
    public DbSet<Studio> Studios { get; set; }
    public DbSet<Dub> Dubs { get; set; }
    public DbSet<Mediaplayer> Mediaplayers { get; set; }
    public DbSet<TagAnime> TagAnimes { get; set; }
    public DbSet<CountryAnime> CountryAnimes { get; set; }
    public DbSet<DubAnime> DubAnimes { get; set; }
    public DbSet<Anime> Animes { get; set; }
    public HikkaDbContext(DbContextOptions<HikkaDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the self-referencing relationship
        modelBuilder.Entity<Tag>()
           .HasOne(t => t.ParentTag)
           .WithMany(t => t.Tags)
           .HasForeignKey("ParentId");

        modelBuilder.Entity<Tag>().Navigation(e => e.SeoAddition).AutoInclude();

        modelBuilder.Entity<Period>().Navigation(e => e.SeoAddition).AutoInclude();

        modelBuilder.Entity<Format>().Navigation(e => e.SeoAddition).AutoInclude();

        modelBuilder.Entity<Status>().Navigation(e => e.SeoAddition).AutoInclude();

        modelBuilder.Entity<Kind>().Navigation(e => e.SeoAddition).AutoInclude();

        modelBuilder.Entity<Source>().Navigation(e => e.SeoAddition).AutoInclude();

        modelBuilder.Entity<RestrictedRating>().Navigation(e => e.SeoAddition).AutoInclude();

        modelBuilder.Entity<Country>().Navigation(e => e.SeoAddition).AutoInclude();

        modelBuilder.Entity<Studio>().Navigation(e => e.SeoAddition).AutoInclude();

        modelBuilder.Entity<Dub>().Navigation(e => e.SeoAddition).AutoInclude();

        modelBuilder.Entity<Anime>().Navigation(e => e.SeoAddition).AutoInclude();


        modelBuilder.Entity<Anime>()
   .HasMany(e => e.Tags)
   .WithMany(e => e.Animes)
   .UsingEntity<TagAnime>(
        l => l.HasOne<Tag>().WithMany(e => e.TagAnimes).OnDelete(DeleteBehavior.Cascade),
        r => r.HasOne<Anime>().WithMany(e => e.TagAnimes).OnDelete(DeleteBehavior.Cascade)
    );
        modelBuilder.Entity<Anime>().Navigation(e => e.Tags).AutoInclude();


        modelBuilder.Entity<Anime>()
  .HasMany(e => e.Countries)
  .WithMany(e => e.Animes)
  .UsingEntity<CountryAnime>(
       l => l.HasOne<Country>().WithMany(e => e.CountryAnimes).OnDelete(DeleteBehavior.Cascade),
       r => r.HasOne<Anime>().WithMany(e => e.CountryAnimes).OnDelete(DeleteBehavior.Cascade)
   );
        modelBuilder.Entity<Anime>().Navigation(e => e.Countries).AutoInclude();

        modelBuilder.Entity<Anime>()
         .HasMany(e => e.Dubs)
         .WithMany(e => e.Animes)
         .UsingEntity<DubAnime>(
              l => l.HasOne<Dub>().WithMany(e => e.DubAnimes).OnDelete(DeleteBehavior.Cascade),
              r => r.HasOne<Anime>().WithMany(e => e.DubAnimes).OnDelete(DeleteBehavior.Cascade)
          );
        modelBuilder.Entity<Anime>().Navigation(e => e.Dubs).AutoInclude();



    }
}