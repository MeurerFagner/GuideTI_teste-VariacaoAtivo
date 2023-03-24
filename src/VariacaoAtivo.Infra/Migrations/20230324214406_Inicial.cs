﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VariacaoAtivo.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ativos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstDateTrade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ativos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pregoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAtivo = table.Column<int>(type: "int", nullable: false),
                    DataPregao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Open = table.Column<float>(type: "real", nullable: true),
                    Low = table.Column<float>(type: "real", nullable: true),
                    High = table.Column<float>(type: "real", nullable: true),
                    Close = table.Column<float>(type: "real", nullable: true),
                    Volume = table.Column<int>(type: "int", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pregoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pregoes_Ativos_IdAtivo",
                        column: x => x.IdAtivo,
                        principalTable: "Ativos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pregoes_IdAtivo",
                table: "Pregoes",
                column: "IdAtivo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pregoes");

            migrationBuilder.DropTable(
                name: "Ativos");
        }
    }
}
