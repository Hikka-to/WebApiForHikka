using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiForHikka.EfPersistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimeRatings_Animes_SecondId",
                table: "AnimeRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimeRatings_AspNetUsers_FirstId",
                table: "AnimeRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_CollectionAnimes_Animes_SecondId",
                table: "CollectionAnimes");

            migrationBuilder.DropForeignKey(
                name: "FK_CollectionAnimes_Collections_FirstId",
                table: "CollectionAnimes");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryAnimes_Animes_SecondId",
                table: "CountryAnimes");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryAnimes_Countries_FirstId",
                table: "CountryAnimes");

            migrationBuilder.DropForeignKey(
                name: "FK_DubAnimes_Animes_SecondId",
                table: "DubAnimes");

            migrationBuilder.DropForeignKey(
                name: "FK_DubAnimes_Dubs_FirstId",
                table: "DubAnimes");

            migrationBuilder.DropForeignKey(
                name: "FK_Relateds_AnimeGroups_SecondId",
                table: "Relateds");

            migrationBuilder.DropForeignKey(
                name: "FK_Relateds_Animes_FirstId",
                table: "Relateds");

            migrationBuilder.DropForeignKey(
                name: "FK_Seasons_AnimeGroups_SecondId",
                table: "Seasons");

            migrationBuilder.DropForeignKey(
                name: "FK_Seasons_Animes_FirstId",
                table: "Seasons");

            migrationBuilder.DropForeignKey(
                name: "FK_Similars_Animes_FirstId",
                table: "Similars");

            migrationBuilder.DropForeignKey(
                name: "FK_Similars_Animes_SecondId",
                table: "Similars");

            migrationBuilder.DropForeignKey(
                name: "FK_TagAnimes_Animes_SecondId",
                table: "TagAnimes");

            migrationBuilder.DropForeignKey(
                name: "FK_TagAnimes_Tags_FirstId",
                table: "TagAnimes");

            migrationBuilder.RenameColumn(
                name: "SecondId",
                table: "TagAnimes",
                newName: "AnimeId");

            migrationBuilder.RenameColumn(
                name: "FirstId",
                table: "TagAnimes",
                newName: "TagId");

            migrationBuilder.RenameIndex(
                name: "IX_TagAnimes_SecondId",
                table: "TagAnimes",
                newName: "IX_TagAnimes_AnimeId");

            migrationBuilder.RenameIndex(
                name: "IX_TagAnimes_FirstId_SecondId",
                table: "TagAnimes",
                newName: "IX_TagAnimes_TagId_AnimeId");

            migrationBuilder.RenameColumn(
                name: "SecondId",
                table: "Similars",
                newName: "SecondAnimeId");

            migrationBuilder.RenameColumn(
                name: "FirstId",
                table: "Similars",
                newName: "AnimeId");

            migrationBuilder.RenameIndex(
                name: "IX_Similars_SecondId",
                table: "Similars",
                newName: "IX_Similars_SecondAnimeId");

            migrationBuilder.RenameIndex(
                name: "IX_Similars_FirstId_SecondId",
                table: "Similars",
                newName: "IX_Similars_AnimeId_SecondAnimeId");

            migrationBuilder.RenameColumn(
                name: "SecondId",
                table: "Seasons",
                newName: "AnimeGroupId");

            migrationBuilder.RenameColumn(
                name: "FirstId",
                table: "Seasons",
                newName: "AnimeId");

            migrationBuilder.RenameIndex(
                name: "IX_Seasons_SecondId",
                table: "Seasons",
                newName: "IX_Seasons_AnimeGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Seasons_FirstId_SecondId",
                table: "Seasons",
                newName: "IX_Seasons_AnimeId_AnimeGroupId");

            migrationBuilder.RenameColumn(
                name: "SecondId",
                table: "Relateds",
                newName: "AnimeGroupId");

            migrationBuilder.RenameColumn(
                name: "FirstId",
                table: "Relateds",
                newName: "AnimeId");

            migrationBuilder.RenameIndex(
                name: "IX_Relateds_SecondId",
                table: "Relateds",
                newName: "IX_Relateds_AnimeGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Relateds_FirstId_SecondId",
                table: "Relateds",
                newName: "IX_Relateds_AnimeId_AnimeGroupId");

            migrationBuilder.RenameColumn(
                name: "SecondId",
                table: "DubAnimes",
                newName: "AnimeId");

            migrationBuilder.RenameColumn(
                name: "FirstId",
                table: "DubAnimes",
                newName: "DubId");

            migrationBuilder.RenameIndex(
                name: "IX_DubAnimes_SecondId",
                table: "DubAnimes",
                newName: "IX_DubAnimes_AnimeId");

            migrationBuilder.RenameIndex(
                name: "IX_DubAnimes_FirstId_SecondId",
                table: "DubAnimes",
                newName: "IX_DubAnimes_DubId_AnimeId");

            migrationBuilder.RenameColumn(
                name: "SecondId",
                table: "CountryAnimes",
                newName: "AnimeId");

            migrationBuilder.RenameColumn(
                name: "FirstId",
                table: "CountryAnimes",
                newName: "CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_CountryAnimes_SecondId",
                table: "CountryAnimes",
                newName: "IX_CountryAnimes_AnimeId");

            migrationBuilder.RenameIndex(
                name: "IX_CountryAnimes_FirstId_SecondId",
                table: "CountryAnimes",
                newName: "IX_CountryAnimes_CountryId_AnimeId");

            migrationBuilder.RenameColumn(
                name: "SecondId",
                table: "CollectionAnimes",
                newName: "AnimeId");

            migrationBuilder.RenameColumn(
                name: "FirstId",
                table: "CollectionAnimes",
                newName: "CollectionId");

            migrationBuilder.RenameIndex(
                name: "IX_CollectionAnimes_SecondId",
                table: "CollectionAnimes",
                newName: "IX_CollectionAnimes_AnimeId");

            migrationBuilder.RenameIndex(
                name: "IX_CollectionAnimes_FirstId_SecondId",
                table: "CollectionAnimes",
                newName: "IX_CollectionAnimes_CollectionId_AnimeId");

            migrationBuilder.RenameColumn(
                name: "SecondId",
                table: "AnimeRatings",
                newName: "AnimeId");

            migrationBuilder.RenameColumn(
                name: "FirstId",
                table: "AnimeRatings",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AnimeRatings_SecondId",
                table: "AnimeRatings",
                newName: "IX_AnimeRatings_AnimeId");

            migrationBuilder.RenameIndex(
                name: "IX_AnimeRatings_FirstId_SecondId",
                table: "AnimeRatings",
                newName: "IX_AnimeRatings_UserId_AnimeId");

            migrationBuilder.AddColumn<Guid>(
                name: "RewiewId",
                table: "AnimeRatings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Slug = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AnimeRatingId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Body = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RemovedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AnimeRatings_AnimeRatingId",
                        column: x => x.AnimeRatingId,
                        principalTable: "AnimeRatings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SearchHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Query = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAnimeListTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Slug = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnimeListTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRecomendations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AnimeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRecomendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRecomendations_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRecomendations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserWatchHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProgressTime = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    EpisodeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWatchHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWatchHistories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWatchHistories_Episodes_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AnimeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewLikes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReviewId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsLiked = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewLikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewLikes_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAnimeLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserAnimeListTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsFavorite = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AnimeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnimeLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAnimeLists_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAnimeLists_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAnimeLists_UserAnimeListTypes_UserAnimeListTypeId",
                        column: x => x.UserAnimeListTypeId,
                        principalTable: "UserAnimeListTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AnimeId",
                table: "Notifications",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ResourceId",
                table: "Notifications",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId_AnimeId",
                table: "Notifications",
                columns: new[] { "UserId", "AnimeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReviewLikes_ReviewId",
                table: "ReviewLikes",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewLikes_UserId",
                table: "ReviewLikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_AnimeRatingId",
                table: "Reviews",
                column: "AnimeRatingId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnimeLists_AnimeId",
                table: "UserAnimeLists",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnimeLists_UserAnimeListTypeId",
                table: "UserAnimeLists",
                column: "UserAnimeListTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnimeLists_UserId_AnimeId",
                table: "UserAnimeLists",
                columns: new[] { "UserId", "AnimeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRecomendations_AnimeId",
                table: "UserRecomendations",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRecomendations_UserId",
                table: "UserRecomendations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWatchHistories_EpisodeId",
                table: "UserWatchHistories",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWatchHistories_UserId_EpisodeId",
                table: "UserWatchHistories",
                columns: new[] { "UserId", "EpisodeId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeRatings_Animes_AnimeId",
                table: "AnimeRatings",
                column: "AnimeId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeRatings_AspNetUsers_UserId",
                table: "AnimeRatings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionAnimes_Animes_AnimeId",
                table: "CollectionAnimes",
                column: "AnimeId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionAnimes_Collections_CollectionId",
                table: "CollectionAnimes",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CountryAnimes_Animes_AnimeId",
                table: "CountryAnimes",
                column: "AnimeId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CountryAnimes_Countries_CountryId",
                table: "CountryAnimes",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DubAnimes_Animes_AnimeId",
                table: "DubAnimes",
                column: "AnimeId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DubAnimes_Dubs_DubId",
                table: "DubAnimes",
                column: "DubId",
                principalTable: "Dubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relateds_AnimeGroups_AnimeGroupId",
                table: "Relateds",
                column: "AnimeGroupId",
                principalTable: "AnimeGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relateds_Animes_AnimeId",
                table: "Relateds",
                column: "AnimeId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seasons_AnimeGroups_AnimeGroupId",
                table: "Seasons",
                column: "AnimeGroupId",
                principalTable: "AnimeGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seasons_Animes_AnimeId",
                table: "Seasons",
                column: "AnimeId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Similars_Animes_AnimeId",
                table: "Similars",
                column: "AnimeId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Similars_Animes_SecondAnimeId",
                table: "Similars",
                column: "SecondAnimeId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagAnimes_Animes_AnimeId",
                table: "TagAnimes",
                column: "AnimeId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagAnimes_Tags_TagId",
                table: "TagAnimes",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimeRatings_Animes_AnimeId",
                table: "AnimeRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimeRatings_AspNetUsers_UserId",
                table: "AnimeRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_CollectionAnimes_Animes_AnimeId",
                table: "CollectionAnimes");

            migrationBuilder.DropForeignKey(
                name: "FK_CollectionAnimes_Collections_CollectionId",
                table: "CollectionAnimes");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryAnimes_Animes_AnimeId",
                table: "CountryAnimes");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryAnimes_Countries_CountryId",
                table: "CountryAnimes");

            migrationBuilder.DropForeignKey(
                name: "FK_DubAnimes_Animes_AnimeId",
                table: "DubAnimes");

            migrationBuilder.DropForeignKey(
                name: "FK_DubAnimes_Dubs_DubId",
                table: "DubAnimes");

            migrationBuilder.DropForeignKey(
                name: "FK_Relateds_AnimeGroups_AnimeGroupId",
                table: "Relateds");

            migrationBuilder.DropForeignKey(
                name: "FK_Relateds_Animes_AnimeId",
                table: "Relateds");

            migrationBuilder.DropForeignKey(
                name: "FK_Seasons_AnimeGroups_AnimeGroupId",
                table: "Seasons");

            migrationBuilder.DropForeignKey(
                name: "FK_Seasons_Animes_AnimeId",
                table: "Seasons");

            migrationBuilder.DropForeignKey(
                name: "FK_Similars_Animes_AnimeId",
                table: "Similars");

            migrationBuilder.DropForeignKey(
                name: "FK_Similars_Animes_SecondAnimeId",
                table: "Similars");

            migrationBuilder.DropForeignKey(
                name: "FK_TagAnimes_Animes_AnimeId",
                table: "TagAnimes");

            migrationBuilder.DropForeignKey(
                name: "FK_TagAnimes_Tags_TagId",
                table: "TagAnimes");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "ReviewLikes");

            migrationBuilder.DropTable(
                name: "SearchHistories");

            migrationBuilder.DropTable(
                name: "UserAnimeLists");

            migrationBuilder.DropTable(
                name: "UserRecomendations");

            migrationBuilder.DropTable(
                name: "UserWatchHistories");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "UserAnimeListTypes");

            migrationBuilder.DropColumn(
                name: "RewiewId",
                table: "AnimeRatings");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "TagAnimes",
                newName: "FirstId");

            migrationBuilder.RenameColumn(
                name: "AnimeId",
                table: "TagAnimes",
                newName: "SecondId");

            migrationBuilder.RenameIndex(
                name: "IX_TagAnimes_TagId_AnimeId",
                table: "TagAnimes",
                newName: "IX_TagAnimes_FirstId_SecondId");

            migrationBuilder.RenameIndex(
                name: "IX_TagAnimes_AnimeId",
                table: "TagAnimes",
                newName: "IX_TagAnimes_SecondId");

            migrationBuilder.RenameColumn(
                name: "SecondAnimeId",
                table: "Similars",
                newName: "SecondId");

            migrationBuilder.RenameColumn(
                name: "AnimeId",
                table: "Similars",
                newName: "FirstId");

            migrationBuilder.RenameIndex(
                name: "IX_Similars_SecondAnimeId",
                table: "Similars",
                newName: "IX_Similars_SecondId");

            migrationBuilder.RenameIndex(
                name: "IX_Similars_AnimeId_SecondAnimeId",
                table: "Similars",
                newName: "IX_Similars_FirstId_SecondId");

            migrationBuilder.RenameColumn(
                name: "AnimeId",
                table: "Seasons",
                newName: "FirstId");

            migrationBuilder.RenameColumn(
                name: "AnimeGroupId",
                table: "Seasons",
                newName: "SecondId");

            migrationBuilder.RenameIndex(
                name: "IX_Seasons_AnimeId_AnimeGroupId",
                table: "Seasons",
                newName: "IX_Seasons_FirstId_SecondId");

            migrationBuilder.RenameIndex(
                name: "IX_Seasons_AnimeGroupId",
                table: "Seasons",
                newName: "IX_Seasons_SecondId");

            migrationBuilder.RenameColumn(
                name: "AnimeId",
                table: "Relateds",
                newName: "FirstId");

            migrationBuilder.RenameColumn(
                name: "AnimeGroupId",
                table: "Relateds",
                newName: "SecondId");

            migrationBuilder.RenameIndex(
                name: "IX_Relateds_AnimeId_AnimeGroupId",
                table: "Relateds",
                newName: "IX_Relateds_FirstId_SecondId");

            migrationBuilder.RenameIndex(
                name: "IX_Relateds_AnimeGroupId",
                table: "Relateds",
                newName: "IX_Relateds_SecondId");

            migrationBuilder.RenameColumn(
                name: "DubId",
                table: "DubAnimes",
                newName: "FirstId");

            migrationBuilder.RenameColumn(
                name: "AnimeId",
                table: "DubAnimes",
                newName: "SecondId");

            migrationBuilder.RenameIndex(
                name: "IX_DubAnimes_DubId_AnimeId",
                table: "DubAnimes",
                newName: "IX_DubAnimes_FirstId_SecondId");

            migrationBuilder.RenameIndex(
                name: "IX_DubAnimes_AnimeId",
                table: "DubAnimes",
                newName: "IX_DubAnimes_SecondId");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "CountryAnimes",
                newName: "FirstId");

            migrationBuilder.RenameColumn(
                name: "AnimeId",
                table: "CountryAnimes",
                newName: "SecondId");

            migrationBuilder.RenameIndex(
                name: "IX_CountryAnimes_CountryId_AnimeId",
                table: "CountryAnimes",
                newName: "IX_CountryAnimes_FirstId_SecondId");

            migrationBuilder.RenameIndex(
                name: "IX_CountryAnimes_AnimeId",
                table: "CountryAnimes",
                newName: "IX_CountryAnimes_SecondId");

            migrationBuilder.RenameColumn(
                name: "CollectionId",
                table: "CollectionAnimes",
                newName: "FirstId");

            migrationBuilder.RenameColumn(
                name: "AnimeId",
                table: "CollectionAnimes",
                newName: "SecondId");

            migrationBuilder.RenameIndex(
                name: "IX_CollectionAnimes_CollectionId_AnimeId",
                table: "CollectionAnimes",
                newName: "IX_CollectionAnimes_FirstId_SecondId");

            migrationBuilder.RenameIndex(
                name: "IX_CollectionAnimes_AnimeId",
                table: "CollectionAnimes",
                newName: "IX_CollectionAnimes_SecondId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AnimeRatings",
                newName: "FirstId");

            migrationBuilder.RenameColumn(
                name: "AnimeId",
                table: "AnimeRatings",
                newName: "SecondId");

            migrationBuilder.RenameIndex(
                name: "IX_AnimeRatings_UserId_AnimeId",
                table: "AnimeRatings",
                newName: "IX_AnimeRatings_FirstId_SecondId");

            migrationBuilder.RenameIndex(
                name: "IX_AnimeRatings_AnimeId",
                table: "AnimeRatings",
                newName: "IX_AnimeRatings_SecondId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeRatings_Animes_SecondId",
                table: "AnimeRatings",
                column: "SecondId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeRatings_AspNetUsers_FirstId",
                table: "AnimeRatings",
                column: "FirstId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionAnimes_Animes_SecondId",
                table: "CollectionAnimes",
                column: "SecondId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionAnimes_Collections_FirstId",
                table: "CollectionAnimes",
                column: "FirstId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CountryAnimes_Animes_SecondId",
                table: "CountryAnimes",
                column: "SecondId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CountryAnimes_Countries_FirstId",
                table: "CountryAnimes",
                column: "FirstId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DubAnimes_Animes_SecondId",
                table: "DubAnimes",
                column: "SecondId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DubAnimes_Dubs_FirstId",
                table: "DubAnimes",
                column: "FirstId",
                principalTable: "Dubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relateds_AnimeGroups_SecondId",
                table: "Relateds",
                column: "SecondId",
                principalTable: "AnimeGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relateds_Animes_FirstId",
                table: "Relateds",
                column: "FirstId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seasons_AnimeGroups_SecondId",
                table: "Seasons",
                column: "SecondId",
                principalTable: "AnimeGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seasons_Animes_FirstId",
                table: "Seasons",
                column: "FirstId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Similars_Animes_FirstId",
                table: "Similars",
                column: "FirstId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Similars_Animes_SecondId",
                table: "Similars",
                column: "SecondId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagAnimes_Animes_SecondId",
                table: "TagAnimes",
                column: "SecondId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagAnimes_Tags_FirstId",
                table: "TagAnimes",
                column: "FirstId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
