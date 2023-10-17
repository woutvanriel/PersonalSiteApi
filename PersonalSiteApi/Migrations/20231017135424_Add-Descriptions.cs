using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalSiteApi.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProjectDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PageDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProjectDetails");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PageDetails");
        }
    }
}
