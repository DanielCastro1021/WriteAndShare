using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace web_api_write_and_share.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Upload",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Upload",
                table: "Posts");
        }
    }
}
