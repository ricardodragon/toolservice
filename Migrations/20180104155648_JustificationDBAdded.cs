using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace toolservice.Migrations
{
    public partial class JustificationDBAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StateTransitionHistories_Justification_justificationId",
                table: "StateTransitionHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Justification",
                table: "Justification");

            migrationBuilder.RenameTable(
                name: "Justification",
                newName: "Justifications");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Justifications",
                table: "Justifications",
                column: "justificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_StateTransitionHistories_Justifications_justificationId",
                table: "StateTransitionHistories",
                column: "justificationId",
                principalTable: "Justifications",
                principalColumn: "justificationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StateTransitionHistories_Justifications_justificationId",
                table: "StateTransitionHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Justifications",
                table: "Justifications");

            migrationBuilder.RenameTable(
                name: "Justifications",
                newName: "Justification");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Justification",
                table: "Justification",
                column: "justificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_StateTransitionHistories_Justification_justificationId",
                table: "StateTransitionHistories",
                column: "justificationId",
                principalTable: "Justification",
                principalColumn: "justificationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
