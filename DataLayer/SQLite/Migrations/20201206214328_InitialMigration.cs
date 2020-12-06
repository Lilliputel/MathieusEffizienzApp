using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.SQLite.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DateSpans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Start = table.Column<DateTime>(type: "TEXT", nullable: false),
                    End = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateSpans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTexts",
                columns: table => new
                {
                    Title = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Color = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTexts", x => x.Title);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserTextId = table.Column<string>(type: "TEXT", nullable: false),
                    Archived = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_UserTexts_UserTextId",
                        column: x => x.UserTextId,
                        principalTable: "UserTexts",
                        principalColumn: "Title",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserTextId = table.Column<string>(type: "TEXT", nullable: false),
                    ParentCategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    ParentGoalId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlanId = table.Column<int>(type: "INTEGER", nullable: false),
                    State = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goals_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Goals_DateSpans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "DateSpans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Goals_Goals_ParentGoalId",
                        column: x => x.ParentGoalId,
                        principalTable: "Goals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Goals_UserTexts_UserTextId",
                        column: x => x.UserTextId,
                        principalTable: "UserTexts",
                        principalColumn: "Title",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Times",
                columns: table => new
                {
                    Day = table.Column<string>(type: "TEXT", nullable: false),
                    Start = table.Column<double>(type: "REAL", nullable: false),
                    End = table.Column<double>(type: "REAL", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Times", x => new { x.Day, x.Start, x.End });
                    table.ForeignKey(
                        name: "FK_Times_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkItems",
                columns: table => new
                {
                    Start = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    End = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Time = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    GoalId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkItems", x => new { x.Start, x.End, x.Date });
                    table.ForeignKey(
                        name: "FK_WorkItems_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkItems_Goals_GoalId",
                        column: x => x.GoalId,
                        principalTable: "Goals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserTextId",
                table: "Categories",
                column: "UserTextId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_ParentCategoryId",
                table: "Goals",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_ParentGoalId",
                table: "Goals",
                column: "ParentGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_PlanId",
                table: "Goals",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_UserTextId",
                table: "Goals",
                column: "UserTextId");

            migrationBuilder.CreateIndex(
                name: "IX_Times_CategoryId",
                table: "Times",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItems_CategoryId",
                table: "WorkItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItems_GoalId",
                table: "WorkItems",
                column: "GoalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Times");

            migrationBuilder.DropTable(
                name: "WorkItems");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "DateSpans");

            migrationBuilder.DropTable(
                name: "UserTexts");
        }
    }
}
