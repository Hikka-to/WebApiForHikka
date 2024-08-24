using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiForHikka.EfPersistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCharacterTag",
                table: "Tags",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "CharactersId",
                table: "TagAnimes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(156)", maxLength: 156, nullable: true),
                    RomajiName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    NativeName = table.Column<string>(type: "character varying(156)", maxLength: 156, nullable: false),
                    AlternativeName = table.Column<string>(type: "character varying(156)", maxLength: 156, nullable: true),
                    ImagePath = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    Description = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SeoAdditionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_SeoAdditions_SeoAdditionId",
                        column: x => x.SeoAdditionId,
                        principalTable: "SeoAdditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimeCharacters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AnimeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CharacterId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeCharacters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimeCharacters_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeCharacters_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagCharacters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TagId = table.Column<Guid>(type: "uuid", nullable: false),
                    CharacterId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagCharacters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagCharacters_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagCharacters_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagAnimes_CharactersId",
                table: "TagAnimes",
                column: "CharactersId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeCharacters_AnimeId_CharacterId",
                table: "AnimeCharacters",
                columns: new[] { "AnimeId", "CharacterId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnimeCharacters_CharacterId",
                table: "AnimeCharacters",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_SeoAdditionId",
                table: "Characters",
                column: "SeoAdditionId");

            migrationBuilder.CreateIndex(
                name: "IX_TagCharacters_CharacterId",
                table: "TagCharacters",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_TagCharacters_TagId_CharacterId",
                table: "TagCharacters",
                columns: new[] { "TagId", "CharacterId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TagAnimes_Characters_CharactersId",
                table: "TagAnimes",
                column: "CharactersId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagAnimes_Characters_CharactersId",
                table: "TagAnimes");

            migrationBuilder.DropTable(
                name: "AnimeCharacters");

            migrationBuilder.DropTable(
                name: "TagCharacters");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_TagAnimes_CharactersId",
                table: "TagAnimes");

            migrationBuilder.DropColumn(
                name: "IsCharacterTag",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "CharactersId",
                table: "TagAnimes");
        }
    }
}
