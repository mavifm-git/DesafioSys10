using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Desafio.Data.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Escola",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    Nome = table.Column<string>(type: "varchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escola", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    Nome = table.Column<string>(type: "varchar(250)", nullable: false),
                    Login = table.Column<string>(type: "varchar(50)", nullable: false),
                    Senha = table.Column<string>(type: "varchar(50)", nullable: false),
                    Perfil = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turma",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    Nome = table.Column<string>(type: "varchar(250)", nullable: false),
                    EscolaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turma_Escola_EscolaID",
                        column: x => x.EscolaID,
                        principalTable: "Escola",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    Nome = table.Column<string>(type: "varchar(250)", nullable: false),
                    Nota = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TurmaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aluno_Turma_TurmaID",
                        column: x => x.TurmaID,
                        principalTable: "Turma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Escola",
                columns: new[] { "Id", "DataCriacao", "Nome" },
                values: new object[] { 1, new DateTime(2021, 3, 22, 9, 49, 8, 80, DateTimeKind.Local).AddTicks(9762), "Pedro II" });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "DataCriacao", "Login", "Nome", "Perfil", "Senha" },
                values: new object[] { 1, new DateTime(2021, 3, 22, 9, 49, 8, 78, DateTimeKind.Local).AddTicks(4564), "admin", "Administrador", "Escola", "1234" });

            migrationBuilder.InsertData(
                table: "Turma",
                columns: new[] { "Id", "DataCriacao", "EscolaID", "Nome" },
                values: new object[] { 1, new DateTime(2021, 3, 22, 9, 49, 8, 81, DateTimeKind.Local).AddTicks(1009), 1, "101" });

            migrationBuilder.InsertData(
                table: "Aluno",
                columns: new[] { "Id", "DataCriacao", "Nome", "Nota", "TurmaID" },
                values: new object[] { 1, new DateTime(2021, 3, 22, 9, 49, 8, 81, DateTimeKind.Local).AddTicks(2609), "Marcus Vinicius", 10m, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_TurmaID",
                table: "Aluno",
                column: "TurmaID");

            migrationBuilder.CreateIndex(
                name: "IX_Turma_EscolaID",
                table: "Turma",
                column: "EscolaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Turma");

            migrationBuilder.DropTable(
                name: "Escola");
        }
    }
}
