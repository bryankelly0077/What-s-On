using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WhatsOn.Migrations
{
    public partial class userid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "MyEventLists");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "MyEventLists",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "MyEventLists");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "MyEventLists",
                nullable: true);
        }
    }
}
