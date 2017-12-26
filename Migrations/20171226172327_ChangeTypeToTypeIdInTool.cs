using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace toolservice.Migrations
{
    public partial class ChangeTypeToTypeIdInTool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tools_ToolTypes_typeid",
                table: "Tools");

            migrationBuilder.DropIndex(
                name: "IX_Tools_typeid",
                table: "Tools");

            migrationBuilder.RenameColumn(
                name: "typeid",
                table: "Tools",
                newName: "typeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "typeId",
                table: "Tools",
                newName: "typeid");

            migrationBuilder.CreateIndex(
                name: "IX_Tools_typeid",
                table: "Tools",
                column: "typeid");

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_ToolTypes_typeid",
                table: "Tools",
                column: "typeid",
                principalTable: "ToolTypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
