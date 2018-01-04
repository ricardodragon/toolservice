using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace toolservice.Migrations
{
    public partial class ChangedStatusModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "Tools",
                type: "text",
                nullable: false,
                defaultValue: "available",
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Tools_serialNumber",
                table: "Tools",
                column: "serialNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tools_serialNumber",
                table: "Tools");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "Tools",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "available");
        }
    }
}
