using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducacaoOnline.Alunos.Data.Migrations
{
    /// <inheritdoc />
    public partial class renameDoHistoricoAprendizado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AulasConcluidas");

            migrationBuilder.CreateTable(
                name: "HistoricoAprendizado",
                columns: table => new
                {
                    AulaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MatriculaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataConclusao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoAprendizado", x => new { x.MatriculaId, x.AulaId });
                    table.ForeignKey(
                        name: "FK_HistoricoAprendizado_Matriculas_MatriculaId",
                        column: x => x.MatriculaId,
                        principalTable: "Matriculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricoAprendizado");

            migrationBuilder.CreateTable(
                name: "AulasConcluidas",
                columns: table => new
                {
                    MatriculaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AulaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataConclusao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AulasConcluidas", x => new { x.MatriculaId, x.AulaId });
                    table.ForeignKey(
                        name: "FK_AulasConcluidas_Matriculas_MatriculaId",
                        column: x => x.MatriculaId,
                        principalTable: "Matriculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
