using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalSiteApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Flag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PageDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageDetails_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PageDetails_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectDetailsDB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDetailsDB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectDetailsDB_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectDetailsDB_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PageContent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    ProjectDetailsDBId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageContent_PageDetails_DetailsId",
                        column: x => x.DetailsId,
                        principalTable: "PageDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PageContent_ProjectDetailsDB_ProjectDetailsDBId",
                        column: x => x.ProjectDetailsDBId,
                        principalTable: "ProjectDetailsDB",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectContent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    ProjectDBId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectContent_ProjectDetailsDB_DetailsId",
                        column: x => x.DetailsId,
                        principalTable: "ProjectDetailsDB",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectContent_Projects_ProjectDBId",
                        column: x => x.ProjectDBId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PageContent_DetailsId",
                table: "PageContent",
                column: "DetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_PageContent_ProjectDetailsDBId",
                table: "PageContent",
                column: "ProjectDetailsDBId");

            migrationBuilder.CreateIndex(
                name: "IX_PageDetails_LanguageId",
                table: "PageDetails",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_PageDetails_PageId",
                table: "PageDetails",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_Slug",
                table: "Pages",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectContent_DetailsId",
                table: "ProjectContent",
                column: "DetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectContent_ProjectDBId",
                table: "ProjectContent",
                column: "ProjectDBId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetailsDB_LanguageId",
                table: "ProjectDetailsDB",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetailsDB_ProjectId",
                table: "ProjectDetailsDB",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Slug",
                table: "Projects",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PageContent");

            migrationBuilder.DropTable(
                name: "ProjectContent");

            migrationBuilder.DropTable(
                name: "PageDetails");

            migrationBuilder.DropTable(
                name: "ProjectDetailsDB");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
