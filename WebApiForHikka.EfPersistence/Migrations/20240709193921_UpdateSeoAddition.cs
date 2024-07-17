using Microsoft.EntityFrameworkCore.Migrations;
using WebApiForHikka.Domain.Enums;

#nullable disable

namespace WebApiForHikka.EfPersistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeoAddition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<SocialType>(
                name: "SocialType",
                table: "SeoAdditions",
                type: "social_type",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SocialType",
                table: "SeoAdditions");
        }
    }
}
