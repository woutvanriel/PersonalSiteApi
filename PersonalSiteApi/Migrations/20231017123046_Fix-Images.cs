using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalSiteApi.Migrations
{
    /// <inheritdoc />
    public partial class FixImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageDB_Projects_ProjectDBId",
                table: "ImageDB");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageDB",
                table: "ImageDB");

            migrationBuilder.RenameTable(
                name: "ImageDB",
                newName: "Images");

            migrationBuilder.RenameIndex(
                name: "IX_ImageDB_ProjectDBId",
                table: "Images",
                newName: "IX_Images_ProjectDBId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Projects_ProjectDBId",
                table: "Images",
                column: "ProjectDBId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Projects_ProjectDBId",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "ImageDB");

            migrationBuilder.RenameIndex(
                name: "IX_Images_ProjectDBId",
                table: "ImageDB",
                newName: "IX_ImageDB_ProjectDBId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageDB",
                table: "ImageDB",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageDB_Projects_ProjectDBId",
                table: "ImageDB",
                column: "ProjectDBId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
