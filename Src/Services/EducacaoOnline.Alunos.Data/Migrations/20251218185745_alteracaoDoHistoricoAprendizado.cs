using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducacaoOnline.Alunos.Data.Migrations
{
    /// <inheritdoc />
    public partial class alteracaoDoHistoricoAprendizado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricosAprendizado");

            migrationBuilder.DropIndex(
                name: "IX_AulasConcluidas_AulaId_MatriculaId",
                table: "AulasConcluidas");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataConclusao",
                table: "AulasConcluidas",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataConclusao",
                table: "AulasConcluidas");

            migrationBuilder.CreateTable(
                name: "HistoricosAprendizado",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MatriculaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataConclusao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricosAprendizado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricosAprendizado_Matriculas_MatriculaId",
                        column: x => x.MatriculaId,
                        principalTable: "Matriculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AulasConcluidas_AulaId_MatriculaId",
                table: "AulasConcluidas",
                columns: new[] { "AulaId", "MatriculaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosAprendizado_MatriculaId",
                table: "HistoricosAprendizado",
                column: "MatriculaId",
                unique: true);
        }
    }
}
