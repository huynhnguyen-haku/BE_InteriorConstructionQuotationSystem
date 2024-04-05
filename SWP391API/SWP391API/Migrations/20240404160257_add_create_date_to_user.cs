using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391API.Migrations
{
    public partial class add_create_date_to_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "user",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "user");
        }
    }
}
