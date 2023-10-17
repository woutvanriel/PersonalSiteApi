using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalSiteApi.Migrations
{
    /// <inheritdoc />
    public partial class MultiImagesForProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Projects");

            migrationBuilder.CreateTable(
                name: "ImageDB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectDBId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageDB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageDB_Projects_ProjectDBId",
                        column: x => x.ProjectDBId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageDB_ProjectDBId",
                table: "ImageDB",
                column: "ProjectDBId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageDB");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
