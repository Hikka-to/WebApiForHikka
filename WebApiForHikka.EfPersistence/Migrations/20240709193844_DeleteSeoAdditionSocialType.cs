using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiForHikka.EfPersistence.Migrations
{
    /// <inheritdoc />
    public partial class DeleteSeoAdditionSocialType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CountryAnimes_Countries_Id",
                table: "CountryAnimes");

            migrationBuilder.DropForeignKey(
                name: "FK_DubAnimes_Dubs_Id",
                table: "DubAnimes");

            migrationBuilder.DropForeignKey(
                name: "FK_TagAnimes_Tags_Id",
                table: "TagAnimes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagAnimes",
                table: "TagAnimes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DubAnimes",
                table: "DubAnimes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryAnimes",
                table: "CountryAnimes");

            migrationBuilder.DropColumn(
                name: "SocialType",
                table: "SeoAdditions");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:social_type", "website,article,book,profile,video.other,video.movie,video.episode,video.tv_show,music.song,music.album,music.playlist,music.radio_station");

            migrationBuilder.AddColumn<Guid>(
                name: "FirstId",
                table: "TagAnimes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FirstId",
                table: "DubAnimes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FirstId",
                table: "CountryAnimes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagAnimes",
                table: "TagAnimes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DubAnimes",
                table: "DubAnimes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountryAnimes",
                table: "CountryAnimes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TagAnimes_FirstId",
                table: "TagAnimes",
                column: "FirstId");

            migrationBuilder.CreateIndex(
                name: "IX_DubAnimes_FirstId",
                table: "DubAnimes",
                column: "FirstId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryAnimes_FirstId",
                table: "CountryAnimes",
                column: "FirstId");

            migrationBuilder.AddForeignKey(
                name: "FK_CountryAnimes_Countries_FirstId",
                table: "CountryAnimes",
                column: "FirstId",
                principalTable: "Countries",
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
                name: "FK_TagAnimes_Tags_FirstId",
                table: "TagAnimes",
                column: "FirstId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CountryAnimes_Countries_FirstId",
                table: "CountryAnimes");

            migrationBuilder.DropForeignKey(
                name: "FK_DubAnimes_Dubs_FirstId",
                table: "DubAnimes");

            migrationBuilder.DropForeignKey(
                name: "FK_TagAnimes_Tags_FirstId",
                table: "TagAnimes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagAnimes",
                table: "TagAnimes");

            migrationBuilder.DropIndex(
                name: "IX_TagAnimes_FirstId",
                table: "TagAnimes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DubAnimes",
                table: "DubAnimes");

            migrationBuilder.DropIndex(
                name: "IX_DubAnimes_FirstId",
                table: "DubAnimes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryAnimes",
                table: "CountryAnimes");

            migrationBuilder.DropIndex(
                name: "IX_CountryAnimes_FirstId",
                table: "CountryAnimes");

            migrationBuilder.DropColumn(
                name: "FirstId",
                table: "TagAnimes");

            migrationBuilder.DropColumn(
                name: "FirstId",
                table: "DubAnimes");

            migrationBuilder.DropColumn(
                name: "FirstId",
                table: "CountryAnimes");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:social_type", "website,article,book,profile,video.other,video.movie,video.episode,video.tv_show,music.song,music.album,music.playlist,music.radio_station");

            migrationBuilder.AddColumn<string>(
                name: "SocialType",
                table: "SeoAdditions",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagAnimes",
                table: "TagAnimes",
                columns: new[] { "Id", "SecondId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DubAnimes",
                table: "DubAnimes",
                columns: new[] { "Id", "SecondId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountryAnimes",
                table: "CountryAnimes",
                columns: new[] { "Id", "SecondId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CountryAnimes_Countries_Id",
                table: "CountryAnimes",
                column: "Id",
                principalTable: "Countries",
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
                name: "FK_TagAnimes_Tags_Id",
                table: "TagAnimes",
                column: "Id",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
