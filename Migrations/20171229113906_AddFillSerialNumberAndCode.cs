using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace toolservice.Migrations
{
    public partial class AddFillSerialNumberAndCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "ToolTypes",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "Tools",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "serialNumber",
                table: "Tools",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "code",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "serialNumber",
                table: "Tools");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "ToolTypes",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);
        }
    }
}
