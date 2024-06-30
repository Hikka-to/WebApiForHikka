﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApiForHikka.EfPersistence.Data;

#nullable disable

namespace WebApiForHikka.EfPersistence.Migrations
{
    [DbContext(typeof(HikkaDbContext))]
    partial class HikkaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("2ae998d7-d8b1-4616-a0b3-60d29eca6c90"),
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("b1e76313-b130-44f8-ae76-6aff097064aa"),
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = new Guid("5bf717f2-e546-417f-b33a-40eab3eafc96"),
                            Name = "Banned",
                            NormalizedName = "BANNED"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.Format", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<Guid>("SeoAdditionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SeoAdditionId");

                    b.ToTable("Formats");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.Kind", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Hint")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<Guid>("SeoAdditionId")
                        .HasColumnType("uuid");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.HasIndex("SeoAdditionId");

                    b.ToTable("Kinds");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.Period", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("character varying(24)");

                    b.Property<Guid>("SeoAdditionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SeoAdditionId");

                    b.ToTable("Periods");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.RestrictedRating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Hint")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("Icon")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<Guid>("SeoAdditionId")
                        .HasColumnType("uuid");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SeoAdditionId");

                    b.ToTable("RestrictedRatings");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.SeoAddition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(278)
                        .HasColumnType("character varying(278)");

                    b.Property<string>("Image")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ImageAlt")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("SocialImage")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("SocialImageAlt")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("SocialTitle")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("SocialType")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.ToTable("SeoAdditions");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.Source", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("SeoAdditionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SeoAdditionId");

                    b.ToTable("Sources");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.Status", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<Guid>("SeoAdditionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SeoAdditionId");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.WithSeoAddition.Anime", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<float>("AvgDuration")
                        .HasColumnType("real");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("FirstAirDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("HowManyEpisodes")
                        .HasColumnType("integer");

                    b.Property<string>("ImageName")
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)");

                    b.Property<float>("ImdbScore")
                        .HasColumnType("real");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("boolean");

                    b.Property<Guid>("KindId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastAirDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(156)
                        .HasColumnType("character varying(156)");

                    b.Property<string>("NativeName")
                        .IsRequired()
                        .HasMaxLength(156)
                        .HasColumnType("character varying(156)");

                    b.Property<Guid>("PeriodId")
                        .HasColumnType("uuid");

                    b.Property<List<int>>("PosterColors")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.Property<string>("PosterPath")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)");

                    b.Property<DateTime?>("PublishedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("RestrictedRatingId")
                        .HasColumnType("uuid");

                    b.Property<string>("RomajiName")
                        .HasMaxLength(248)
                        .HasColumnType("character varying(248)");

                    b.Property<Guid>("SeoAdditionId")
                        .HasColumnType("uuid");

                    b.Property<long?>("ShikimoriId")
                        .HasColumnType("bigint");

                    b.Property<float>("ShikimoriScore")
                        .HasColumnType("real");

                    b.Property<Guid>("SourceId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("StatusId")
                        .HasColumnType("uuid");

                    b.Property<long?>("TmdbId")
                        .HasColumnType("bigint");

                    b.Property<float>("TmdbScore")
                        .HasColumnType("real");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("KindId");

                    b.HasIndex("PeriodId");

                    b.HasIndex("RestrictedRatingId");

                    b.HasIndex("SeoAdditionId");

                    b.HasIndex("SourceId");

                    b.HasIndex("StatusId");

                    b.ToTable("Animes");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.WithSeoAddition.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<Guid>("SeoAdditionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SeoAdditionId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.WithSeoAddition.Dub", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Icon")
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<Guid>("SeoAdditionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SeoAdditionId");

                    b.ToTable("Dubs");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.WithSeoAddition.Studio", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Logo")
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<Guid>("SeoAdditionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SeoAdditionId");

                    b.ToTable("Studios");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.WithSeoAddition.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<List<string>>("Alises")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("EngName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<bool>("IsGenre")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SeoAdditionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("SeoAdditionId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.WithoutSeoAddition.AnimeBackdrop", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AnimeId")
                        .HasColumnType("uuid");

                    b.Property<List<int>>("Colors")
                        .HasColumnType("integer[]");

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)");

                    b.Property<int>("Width")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AnimeId");

                    b.ToTable("AnimeBackdrops");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.WithoutSeoAddition.AnimeVideoKind", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(156)
                        .HasColumnType("character varying(156)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("AnimeVideoKinds");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.WithoutSeoAddition.CountryAnime", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AnimesId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CountriesId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AnimesId");

                    b.HasIndex("CountriesId");

                    b.ToTable("CountriesAnimes");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.WithoutSeoAddition.DubAnime", b =>
                {
                    b.Property<Guid>("AnimesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DubsId")
                        .HasColumnType("uuid");

                    b.HasKey("AnimesId", "DubsId");

                    b.HasIndex("DubsId");

                    b.ToTable("DubsAnimes");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.WithoutSeoAddition.Mediaplayer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.ToTable("Mediaplayers");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.WithoutSeoAddition.TagAnime", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AnimesId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("TagsId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AnimesId");

                    b.HasIndex("TagsId");

                    b.ToTable("TagsAnimes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("WebApiForHikka.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("WebApiForHikka.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiForHikka.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("WebApiForHikka.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.Format", b =>
                {
                    b.HasOne("WebApiForHikka.Domain.Models.SeoAddition", "SeoAddition")
                        .WithMany()
                        .HasForeignKey("SeoAdditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SeoAddition");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.Kind", b =>
                {
                    b.HasOne("WebApiForHikka.Domain.Models.SeoAddition", "SeoAddition")
                        .WithMany()
                        .HasForeignKey("SeoAdditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SeoAddition");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.Period", b =>
                {
                    b.HasOne("WebApiForHikka.Domain.Models.SeoAddition", "SeoAddition")
                        .WithMany()
                        .HasForeignKey("SeoAdditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SeoAddition");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.RestrictedRating", b =>
                {
                    b.HasOne("WebApiForHikka.Domain.Models.SeoAddition", "SeoAddition")
                        .WithMany()
                        .HasForeignKey("SeoAdditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SeoAddition");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.Source", b =>
                {
                    b.HasOne("WebApiForHikka.Domain.Models.SeoAddition", "SeoAddition")
                        .WithMany()
                        .HasForeignKey("SeoAdditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SeoAddition");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.Status", b =>
                {
                    b.HasOne("WebApiForHikka.Domain.Models.SeoAddition", "SeoAddition")
                        .WithMany()
                        .HasForeignKey("SeoAdditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SeoAddition");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.WithSeoAddition.Anime", b =>
                {
                    b.HasOne("WebApiForHikka.Domain.Models.Kind", "Kind")
                        .WithMany()
                        .HasForeignKey("KindId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiForHikka.Domain.Models.Period", "Period")
                        .WithMany()
                        .HasForeignKey("PeriodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiForHikka.Domain.Models.RestrictedRating", "RestrictedRating")
                        .WithMany()
                        .HasForeignKey("RestrictedRatingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiForHikka.Domain.Models.SeoAddition", "SeoAddition")
                        .WithMany()
                        .HasForeignKey("SeoAdditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiForHikka.Domain.Models.Source", "Source")
                        .WithMany()
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiForHikka.Domain.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kind");

                    b.Navigation("Period");

                    b.Navigation("RestrictedRating");

                    b.Navigation("SeoAddition");

                    b.Navigation("Source");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.WithSeoAddition.Country", b =>
                {
                    b.HasOne("WebApiForHikka.Domain.Models.SeoAddition", "SeoAddition")
                        .WithMany()
                        .HasForeignKey("SeoAdditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SeoAddition");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.WithSeoAddition.Dub", b =>
                {
                    b.HasOne("WebApiForHikka.Domain.Models.SeoAddition", "SeoAddition")
                        .WithMany()
                        .HasForeignKey("SeoAdditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SeoAddition");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.WithSeoAddition.Studio", b =>
                {
                    b.HasOne("WebApiForHikka.Domain.Models.SeoAddition", "SeoAddition")
                        .WithMany()
                        .HasForeignKey("SeoAdditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SeoAddition");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.WithSeoAddition.Tag", b =>
                {
                    b.HasOne("WebApiForHikka.Domain.Models.WithSeoAddition.Tag", "ParentTag")
                        .WithMany("Tags")
                        .HasForeignKey("ParentId");

                    b.HasOne("WebApiForHikka.Domain.Models.SeoAddition", "SeoAddition")
                        .WithMany()
                        .HasForeignKey("SeoAdditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentTag");

                    b.Navigation("SeoAddition");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.WithoutSeoAddition.AnimeBackdrop", b =>
                {
                    b.HasOne("WebApiForHikka.Domain.Models.WithSeoAddition.Anime", "Anime")
                        .WithMany()
                        .HasForeignKey("AnimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Anime");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.WithoutSeoAddition.CountryAnime", b =>
                {
                    b.HasOne("WebApiForHikka.Domain.Models.WithSeoAddition.Anime", null)
                        .WithMany("CountriesAnimes")
                        .HasForeignKey("AnimesId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApiForHikka.Domain.Models.WithSeoAddition.Country", null)
                        .WithMany("CountriesAnimes")
                        .HasForeignKey("CountriesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.WithoutSeoAddition.DubAnime", b =>
                {
                    b.HasOne("WebApiForHikka.Domain.Models.WithSeoAddition.Anime", null)
                        .WithMany("DubsAnimes")
                        .HasForeignKey("AnimesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiForHikka.Domain.Models.WithSeoAddition.Dub", null)
                        .WithMany("DubsAnimes")
                        .HasForeignKey("DubsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.WithoutSeoAddition.TagAnime", b =>
                {
                    b.HasOne("WebApiForHikka.Domain.Models.WithSeoAddition.Anime", null)
                        .WithMany("TagsAnimes")
                        .HasForeignKey("AnimesId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApiForHikka.Domain.Models.WithSeoAddition.Tag", null)
                        .WithMany("TagsAnimes")
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.WithSeoAddition.Anime", b =>
                {
                    b.Navigation("CountriesAnimes");

                    b.Navigation("DubsAnimes");

                    b.Navigation("TagsAnimes");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.WithSeoAddition.Country", b =>
                {
                    b.Navigation("CountriesAnimes");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.WithSeoAddition.Dub", b =>
                {
                    b.Navigation("DubsAnimes");
                });

            modelBuilder.Entity("WebApiForHikka.Domain.Models.WithSeoAddition.Tag", b =>
                {
                    b.Navigation("TagsAnimes");

                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
