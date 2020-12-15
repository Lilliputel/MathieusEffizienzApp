using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.SQLite.Migrations
{
    public partial class AddNullabilityToGoals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Categories_ParentCategoryId",
                table: "Goals");

            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Goals_ParentGoalId",
                table: "Goals");

            migrationBuilder.AlterColumn<int>(
                name: "ParentGoalId",
                table: "Goals",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ParentCategoryId",
                table: "Goals",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Categories_ParentCategoryId",
                table: "Goals",
                column: "ParentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Goals_ParentGoalId",
                table: "Goals",
                column: "ParentGoalId",
                principalTable: "Goals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Categories_ParentCategoryId",
                table: "Goals");

            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Goals_ParentGoalId",
                table: "Goals");

            migrationBuilder.AlterColumn<int>(
                name: "ParentGoalId",
                table: "Goals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ParentCategoryId",
                table: "Goals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Categories_ParentCategoryId",
                table: "Goals",
                column: "ParentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Goals_ParentGoalId",
                table: "Goals",
                column: "ParentGoalId",
                principalTable: "Goals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
