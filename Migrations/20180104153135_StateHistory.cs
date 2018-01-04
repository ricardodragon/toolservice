using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace toolservice.Migrations
{
    public partial class StateHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Justification",
                columns: table => new
                {
                    justificationId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    text = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Justification", x => x.justificationId);
                });

            migrationBuilder.CreateTable(
                name: "StateTransitionHistories",
                columns: table => new
                {
                    stateTransitionHistoryId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    justificationId = table.Column<int>(type: "int4", nullable: true),
                    justificationNeeded = table.Column<bool>(type: "bool", nullable: false),
                    nextState = table.Column<string>(type: "text", nullable: true),
                    previousState = table.Column<string>(type: "text", nullable: true),
                    timeStampTicks = table.Column<long>(type: "int8", nullable: false),
                    toolId = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateTransitionHistories", x => x.stateTransitionHistoryId);
                    table.ForeignKey(
                        name: "FK_StateTransitionHistories_Justification_justificationId",
                        column: x => x.justificationId,
                        principalTable: "Justification",
                        principalColumn: "justificationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StateTransitionHistories_justificationId",
                table: "StateTransitionHistories",
                column: "justificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StateTransitionHistories");

            migrationBuilder.DropTable(
                name: "Justification");
        }
    }
}
