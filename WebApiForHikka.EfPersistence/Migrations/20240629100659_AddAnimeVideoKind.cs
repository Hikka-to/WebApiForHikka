using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiForHikka.EfPersistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAnimeVideoKind : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimeVideoKinds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(156)", maxLength: 156, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeVideoKinds", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimeVideoKinds_Name",
                table: "AnimeVideoKinds",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimeVideoKinds");
        }
    }
}
