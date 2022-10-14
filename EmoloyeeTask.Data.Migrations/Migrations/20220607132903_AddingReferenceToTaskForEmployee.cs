using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmoloyeeTask.Data.Migrations.Migrations
{
    public partial class AddingReferenceToTaskForEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Tasks_TaskForEmployeeId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "TaskForEmployeeId",
                table: "Employees",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Tasks_TaskForEmployeeId",
                table: "Employees",
                column: "TaskForEmployeeId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Tasks_TaskForEmployeeId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "TaskForEmployeeId",
                table: "Employees",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Tasks_TaskForEmployeeId",
                table: "Employees",
                column: "TaskForEmployeeId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }
    }
}
