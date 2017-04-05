using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WhatsOn.Migrations
{
    public partial class useridc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyEventId",
                table: "MyEventLists");

            migrationBuilder.AddColumn<string>(
                name: "MyEventUserId",
                table: "MyEventLists",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyEventUserId",
                table: "MyEventLists");

            migrationBuilder.AddColumn<string>(
                name: "MyEventId",
                table: "MyEventLists",
                nullable: true);
        }
    }
}
