using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiForHikka.EfPersistence.Migrations
{
    /// <inheritdoc />
    public partial class AnotherUpdateManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_TagAnimes_Animes_AnimeId",
                table: "TagAnimes");

            migrationBuilder.DropForeignKey(
                name: "FK_TagAnimes_Tags_TagId",
                table: "TagAnimes");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "TagAnimes",
                newName: "SecondId");

            migrationBuilder.RenameColumn(
                name: "AnimeId",
                table: "TagAnimes",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_TagAnimes_TagId",
                table: "TagAnimes",
                newName: "IX_TagAnimes_SecondId");

            migrationBuilder.RenameColumn(
                name: "DubId",
                table: "DubAnimes",
                newName: "SecondId");

            migrationBuilder.RenameColumn(
                name: "AnimeId",
                table: "DubAnimes",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_DubAnimes_DubId",
                table: "DubAnimes",
                newName: "IX_DubAnimes_SecondId");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "CountryAnimes",
                newName: "SecondId");

            migrationBuilder.RenameColumn(
                name: "AnimeId",
                table: "CountryAnimes",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_CountryAnimes_CountryId",
                table: "CountryAnimes",
                newName: "IX_CountryAnimes_SecondId");

            migrationBuilder.AddForeignKey(
                name: "FK_CountryAnimes_Animes_SecondId",
                table: "CountryAnimes",
                column: "SecondId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CountryAnimes_Countries_Id",
                table: "CountryAnimes",
                column: "Id",
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
                name: "FK_DubAnimes_Dubs_Id",
                table: "DubAnimes",
                column: "Id",
                principalTable: "Dubs",
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
                name: "FK_TagAnimes_Tags_Id",
                table: "TagAnimes",
                column: "Id",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CountryAnimes_Animes_SecondId",
                table: "CountryAnimes");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryAnimes_Countries_Id",
                table: "CountryAnimes");

            migrationBuilder.DropForeignKey(
                name: "FK_DubAnimes_Animes_SecondId",
                table: "DubAnimes");

            migrationBuilder.DropForeignKey(
                name: "FK_DubAnimes_Dubs_Id",
                table: "DubAnimes");

            migrationBuilder.DropForeignKey(
                name: "FK_TagAnimes_Animes_SecondId",
                table: "TagAnimes");

            migrationBuilder.DropForeignKey(
                name: "FK_TagAnimes_Tags_Id",
                table: "TagAnimes");

            migrationBuilder.RenameColumn(
                name: "SecondId",
                table: "TagAnimes",
                newName: "TagId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TagAnimes",
                newName: "AnimeId");

            migrationBuilder.RenameIndex(
                name: "IX_TagAnimes_SecondId",
                table: "TagAnimes",
                newName: "IX_TagAnimes_TagId");

            migrationBuilder.RenameColumn(
                name: "SecondId",
                table: "DubAnimes",
                newName: "DubId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DubAnimes",
                newName: "AnimeId");

            migrationBuilder.RenameIndex(
                name: "IX_DubAnimes_SecondId",
                table: "DubAnimes",
                newName: "IX_DubAnimes_DubId");

            migrationBuilder.RenameColumn(
                name: "SecondId",
                table: "CountryAnimes",
                newName: "CountryId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CountryAnimes",
                newName: "AnimeId");

            migrationBuilder.RenameIndex(
                name: "IX_CountryAnimes_SecondId",
                table: "CountryAnimes",
                newName: "IX_CountryAnimes_CountryId");

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
    }
}
