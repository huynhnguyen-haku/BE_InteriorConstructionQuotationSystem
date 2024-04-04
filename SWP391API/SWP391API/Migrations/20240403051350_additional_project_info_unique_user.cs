using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391API.Migrations
{
    public partial class additional_project_info_unique_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "status",
                table: "user",
                type: "bit",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValueSql: "((1))");

            migrationBuilder.AddColumn<string>(
                name: "location",
                table: "completedProject",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "project_information",
                table: "completedProject",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "project_result",
                table: "completedProject",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "surface_area",
                table: "completedProject",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "UQ__user__AB6E6164D4A1A3A3",
                table: "user",
                column: "email",
                unique: true,
                filter: "[email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__user__F3DBC572D3A3E3A3",
                table: "user",
                column: "username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ__user__AB6E6164D4A1A3A3",
                table: "user");

            migrationBuilder.DropIndex(
                name: "UQ__user__F3DBC572D3A3E3A3",
                table: "user");

            migrationBuilder.DropColumn(
                name: "location",
                table: "completedProject");

            migrationBuilder.DropColumn(
                name: "project_information",
                table: "completedProject");

            migrationBuilder.DropColumn(
                name: "project_result",
                table: "completedProject");

            migrationBuilder.DropColumn(
                name: "surface_area",
                table: "completedProject");

            migrationBuilder.AlterColumn<bool>(
                name: "status",
                table: "user",
                type: "bit",
                nullable: true,
                defaultValueSql: "((1))",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: false);
        }
    }
}
