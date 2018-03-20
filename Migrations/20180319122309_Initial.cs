using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace toolservice.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Justifications",
                columns: table => new
                {
                    justificationId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Justifications", x => x.justificationId);
                });

            migrationBuilder.CreateTable(
                name: "Tools",
                columns: table => new
                {
                    toolId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    code = table.Column<string>(maxLength: 100, nullable: true),
                    codeClient = table.Column<string>(maxLength: 100, nullable: true),
                    currentLife = table.Column<double>(nullable: false),
                    currentThingId = table.Column<int>(nullable: true),
                    description = table.Column<string>(maxLength: 100, nullable: true),
                    lifeCycle = table.Column<double>(nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: true),
                    serialNumber = table.Column<string>(maxLength: 100, nullable: true),
                    status = table.Column<string>(nullable: false, defaultValue: "available"),
                    typeId = table.Column<int>(nullable: false),
                    unitOfMeasurement = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tools", x => x.toolId);
                });

            migrationBuilder.CreateTable(
                name: "ToolTypes",
                columns: table => new
                {
                    toolTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    description = table.Column<string>(maxLength: 100, nullable: true),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    status = table.Column<string>(nullable: false),
                    thingGroupIds = table.Column<int[]>(type: "integer[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolTypes", x => x.toolTypeId);
                });

            migrationBuilder.CreateTable(
                name: "StateTransitionHistories",
                columns: table => new
                {
                    stateTransitionHistoryId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    justificationId = table.Column<int>(nullable: true),
                    justificationNeeded = table.Column<bool>(nullable: false),
                    nextState = table.Column<string>(nullable: true),
                    previousState = table.Column<string>(nullable: true),
                    previoustLife = table.Column<double>(nullable: false),
                    timeStampTicks = table.Column<long>(nullable: false),
                    toolId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateTransitionHistories", x => x.stateTransitionHistoryId);
                    table.ForeignKey(
                        name: "FK_StateTransitionHistories_Justifications_justificationId",
                        column: x => x.justificationId,
                        principalTable: "Justifications",
                        principalColumn: "justificationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ToolInformations",
                columns: table => new
                {
                    toolInformationId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    datetime = table.Column<long>(nullable: false),
                    toolId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolInformations", x => x.toolInformationId);
                    table.ForeignKey(
                        name: "FK_ToolInformations_Tools_toolId",
                        column: x => x.toolId,
                        principalTable: "Tools",
                        principalColumn: "toolId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ToolInformationAdditional",
                columns: table => new
                {
                    toolInformationAdditionalId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    key = table.Column<string>(nullable: true),
                    toolInformationId = table.Column<int>(nullable: true),
                    value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolInformationAdditional", x => x.toolInformationAdditionalId);
                    table.ForeignKey(
                        name: "FK_ToolInformationAdditional_ToolInformations_toolInformationId",
                        column: x => x.toolInformationId,
                        principalTable: "ToolInformations",
                        principalColumn: "toolInformationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StateTransitionHistories_justificationId",
                table: "StateTransitionHistories",
                column: "justificationId");

            migrationBuilder.CreateIndex(
                name: "IX_ToolInformationAdditional_toolInformationId",
                table: "ToolInformationAdditional",
                column: "toolInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_ToolInformations_toolId",
                table: "ToolInformations",
                column: "toolId");

            migrationBuilder.CreateIndex(
                name: "IX_Tools_serialNumber",
                table: "Tools",
                column: "serialNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StateTransitionHistories");

            migrationBuilder.DropTable(
                name: "ToolInformationAdditional");

            migrationBuilder.DropTable(
                name: "ToolTypes");

            migrationBuilder.DropTable(
                name: "Justifications");

            migrationBuilder.DropTable(
                name: "ToolInformations");

            migrationBuilder.DropTable(
                name: "Tools");
        }
    }
}
