using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiForHikka.EfPersistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountriesAnimes");

            migrationBuilder.DropTable(
                name: "DubsAnimes");

            migrationBuilder.DropTable(
                name: "TagsAnimes");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Sources",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CountryAnimes",
                columns: table => new
                {
                    AnimeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryAnimes", x => new { x.AnimeId, x.CountryId });
                    table.ForeignKey(
                        name: "FK_CountryAnimes_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryAnimes_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DubAnimes",
                columns: table => new
                {
                    AnimeId = table.Column<Guid>(type: "uuid", nullable: false),
                    DubId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DubAnimes", x => new { x.AnimeId, x.DubId });
                    table.ForeignKey(
                        name: "FK_DubAnimes_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DubAnimes_Dubs_DubId",
                        column: x => x.DubId,
                        principalTable: "Dubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagAnimes",
                columns: table => new
                {
                    AnimeId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagAnimes", x => new { x.AnimeId, x.TagId });
                    table.ForeignKey(
                        name: "FK_TagAnimes_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagAnimes_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CountryAnimes_CountryId",
                table: "CountryAnimes",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_DubAnimes_DubId",
                table: "DubAnimes",
                column: "DubId");

            migrationBuilder.CreateIndex(
                name: "IX_TagAnimes_TagId",
                table: "TagAnimes",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountryAnimes");

            migrationBuilder.DropTable(
                name: "DubAnimes");

            migrationBuilder.DropTable(
                name: "TagAnimes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Sources");

            migrationBuilder.CreateTable(
                name: "CountriesAnimes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AnimesId = table.Column<Guid>(type: "uuid", nullable: true),
                    CountriesId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountriesAnimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountriesAnimes_Animes_AnimesId",
                        column: x => x.AnimesId,
                        principalTable: "Animes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountriesAnimes_Countries_CountriesId",
                        column: x => x.CountriesId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DubsAnimes",
                columns: table => new
                {
                    AnimesId = table.Column<Guid>(type: "uuid", nullable: false),
                    DubsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DubsAnimes", x => new { x.AnimesId, x.DubsId });
                    table.ForeignKey(
                        name: "FK_DubsAnimes_Animes_AnimesId",
                        column: x => x.AnimesId,
                        principalTable: "Animes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DubsAnimes_Dubs_DubsId",
                        column: x => x.DubsId,
                        principalTable: "Dubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagsAnimes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AnimesId = table.Column<Guid>(type: "uuid", nullable: true),
                    TagsId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagsAnimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagsAnimes_Animes_AnimesId",
                        column: x => x.AnimesId,
                        principalTable: "Animes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagsAnimes_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CountriesAnimes_AnimesId",
                table: "CountriesAnimes",
                column: "AnimesId");

            migrationBuilder.CreateIndex(
                name: "IX_CountriesAnimes_CountriesId",
                table: "CountriesAnimes",
                column: "CountriesId");

            migrationBuilder.CreateIndex(
                name: "IX_DubsAnimes_DubsId",
                table: "DubsAnimes",
                column: "DubsId");

            migrationBuilder.CreateIndex(
                name: "IX_TagsAnimes_AnimesId",
                table: "TagsAnimes",
                column: "AnimesId");

            migrationBuilder.CreateIndex(
                name: "IX_TagsAnimes_TagsId",
                table: "TagsAnimes",
                column: "TagsId");
        }
    }
}
