using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace toolservice.Migrations
{
    public partial class ChangedLifeCycleToType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lifeCycle",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "unitOfMeasurement",
                table: "Tools");

            migrationBuilder.AddColumn<double>(
                name: "lifeCycle",
                table: "ToolTypes",
                nullable: false,
                defaultValue: 0.0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tools_ToolTypes_toolTypeId",
                table: "Tools");

            migrationBuilder.DropIndex(
                name: "IX_Tools_toolTypeId",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "lifeCycle",
                table: "ToolTypes");

            migrationBuilder.DropColumn(
                name: "toolTypeId",
                table: "Tools");

            migrationBuilder.AddColumn<double>(
                name: "lifeCycle",
                table: "Tools",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "unitOfMeasurement",
                table: "Tools",
                nullable: false,
                defaultValue: "");
        }
    }
}
