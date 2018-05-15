using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace toolservice.Migrations
{
    public partial class CorrectionOnMap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tools_ToolTypes_toolTypeId",
                table: "Tools");

            migrationBuilder.DropIndex(
                name: "IX_Tools_toolTypeId",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "toolTypeId",
                table: "Tools");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "toolTypeId",
                table: "Tools",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tools_toolTypeId",
                table: "Tools",
                column: "toolTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_ToolTypes_toolTypeId",
                table: "Tools",
                column: "toolTypeId",
                principalTable: "ToolTypes",
                principalColumn: "toolTypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
