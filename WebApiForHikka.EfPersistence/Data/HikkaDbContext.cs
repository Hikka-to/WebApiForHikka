using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.EfPersistence.Data;

public class HikkaDbContext(DbContextOptions<HikkaDbContext> options)
    : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
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
    public DbSet<AnimeVideoKind> AnimeVideoKinds { get; set; }
    public DbSet<AnimeVideo> AnimeVideos { get; set; }
    public DbSet<AlternativeName> AlternativeNames { get; set; }
    public DbSet<ExternalLink> ExternalLinks { get; set; }
    public DbSet<RelatedType> RelatedTypes { get; set; }
    public DbSet<AnimeGroup> AnimeGroups { get; set; }
    public DbSet<Related> Relateds { get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<Similar> Similars { get; set; }
    public DbSet<Episode> Episodes { get; set; }
    public DbSet<EpisodeImage> EpisodeImages { get; set; }
    public DbSet<Collection> Collections { get; set; }
    public DbSet<CollectionAnime> CollectionAnimes { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<LanguageMediaplayer> LanguageMediaplayers { get; set; }
    public DbSet<Provider> Providers { get; set; }
    public DbSet<UserSetting> UserSettings { get; set; }
    public DbSet<EmojiGroup> EmojiGroups { get; set; }

    public DbSet<AnimeRating> AnimeRatings { get; set; }

    public DbSet<CommentReportType> CommentReportTypes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Enums
        var modelsAssembly = typeof(IModel).Assembly;
        var enumTypes = modelsAssembly.GetTypes().Where(t =>
            (t.Namespace?.Contains("Enums") ?? false) && t.IsEnum);
        var hasPostgresEnum = typeof(NpgsqlModelBuilderExtensions).GetMethods().First(m => m is
        {
            Name: nameof(NpgsqlModelBuilderExtensions.HasPostgresEnum),
            IsGenericMethod: true
        });
        foreach (var enumType in enumTypes)
            hasPostgresEnum.MakeGenericMethod(enumType).Invoke(null, [modelBuilder, null, null, null]);

        // Comments
        modelBuilder.Entity<Commentable>().UseTptMappingStrategy();

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

        //User
        modelBuilder.Entity<User>()
            .HasMany(e => e.Roles)
            .WithMany()
            .UsingEntity<IdentityUserRole<Guid>>();

        //Anime
        modelBuilder.Entity<Anime>()
            .HasMany(e => e.Tags)
            .WithMany(e => e.Animes)
            .UsingEntity<TagAnime>();

        modelBuilder.Entity<Anime>()
            .HasMany(e => e.Countries)
            .WithMany(e => e.Animes)
            .UsingEntity<CountryAnime>();

        modelBuilder.Entity<Anime>()
            .HasMany(e => e.Dubs)
            .WithMany(e => e.Animes)
            .UsingEntity<DubAnime>();

        modelBuilder.Entity<Anime>()
            .HasMany(e => e.RelatedAnimeGroups)
            .WithMany(e => e.RelatedAnimes)
            .UsingEntity<Related>();

        modelBuilder.Entity<Anime>()
            .HasMany(e => e.SeasonAnimeGroups)
            .WithMany(e => e.SeasonAnimes)
            .UsingEntity<Season>();

        modelBuilder.Entity<Anime>()
            .HasMany(e => e.SimilarChildAnimes)
            .WithMany(e => e.SimilarParentAnimes)
            .UsingEntity<Similar>(
                r => r.HasOne(e => e.First).WithMany(),
                l => l.HasOne(e => e.Second).WithMany());

        modelBuilder.Entity<Anime>()
            .HasMany(e => e.Collections)
            .WithMany(e => e.Animes)
            .UsingEntity<CollectionAnime>();
    }
}