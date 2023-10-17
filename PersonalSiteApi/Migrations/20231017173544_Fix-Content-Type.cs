using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalSiteApi.Migrations
{
    /// <inheritdoc />
    public partial class FixContentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageContent_ProjectDetails_ProjectDetailsDBId",
                table: "PageContent");

            migrationBuilder.DropIndex(
                name: "IX_PageContent_ProjectDetailsDBId",
                table: "PageContent");

            migrationBuilder.DropColumn(
                name: "ProjectDetailsDBId",
                table: "PageContent");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProjectDetailsDBId",
                table: "PageContent",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PageContent_ProjectDetailsDBId",
                table: "PageContent",
                column: "ProjectDetailsDBId");

            migrationBuilder.AddForeignKey(
                name: "FK_PageContent_ProjectDetails_ProjectDetailsDBId",
                table: "PageContent",
                column: "ProjectDetailsDBId",
                principalTable: "ProjectDetails",
                principalColumn: "Id");
        }
    }
}
