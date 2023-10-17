using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalSiteApi.Migrations
{
    /// <inheritdoc />
    public partial class FixModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageContent_ProjectDetailsDB_ProjectDetailsDBId",
                table: "PageContent");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectContent_ProjectDetailsDB_DetailsId",
                table: "ProjectContent");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDetailsDB_Languages_LanguageId",
                table: "ProjectDetailsDB");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDetailsDB_Projects_ProjectId",
                table: "ProjectDetailsDB");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectDetailsDB",
                table: "ProjectDetailsDB");

            migrationBuilder.RenameTable(
                name: "ProjectDetailsDB",
                newName: "ProjectDetails");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectDetailsDB_ProjectId",
                table: "ProjectDetails",
                newName: "IX_ProjectDetails_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectDetailsDB_LanguageId",
                table: "ProjectDetails",
                newName: "IX_ProjectDetails_LanguageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectDetails",
                table: "ProjectDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PageContent_ProjectDetails_ProjectDetailsDBId",
                table: "PageContent",
                column: "ProjectDetailsDBId",
                principalTable: "ProjectDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectContent_ProjectDetails_DetailsId",
                table: "ProjectContent",
                column: "DetailsId",
                principalTable: "ProjectDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDetails_Languages_LanguageId",
                table: "ProjectDetails",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDetails_Projects_ProjectId",
                table: "ProjectDetails",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageContent_ProjectDetails_ProjectDetailsDBId",
                table: "PageContent");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectContent_ProjectDetails_DetailsId",
                table: "ProjectContent");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDetails_Languages_LanguageId",
                table: "ProjectDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDetails_Projects_ProjectId",
                table: "ProjectDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectDetails",
                table: "ProjectDetails");

            migrationBuilder.RenameTable(
                name: "ProjectDetails",
                newName: "ProjectDetailsDB");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectDetails_ProjectId",
                table: "ProjectDetailsDB",
                newName: "IX_ProjectDetailsDB_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectDetails_LanguageId",
                table: "ProjectDetailsDB",
                newName: "IX_ProjectDetailsDB_LanguageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectDetailsDB",
                table: "ProjectDetailsDB",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PageContent_ProjectDetailsDB_ProjectDetailsDBId",
                table: "PageContent",
                column: "ProjectDetailsDBId",
                principalTable: "ProjectDetailsDB",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectContent_ProjectDetailsDB_DetailsId",
                table: "ProjectContent",
                column: "DetailsId",
                principalTable: "ProjectDetailsDB",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDetailsDB_Languages_LanguageId",
                table: "ProjectDetailsDB",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDetailsDB_Projects_ProjectId",
                table: "ProjectDetailsDB",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
