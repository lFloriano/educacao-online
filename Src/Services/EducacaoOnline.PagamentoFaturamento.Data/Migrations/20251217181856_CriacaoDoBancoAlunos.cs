using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducacaoOnline.PagamentoFaturamento.Data.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoDoBancoAlunos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pagamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AlunoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CursoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Valor = table.Column<decimal>(type: "TEXT", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CartaoTitular = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    CartaoNumero = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    CartaoValidade = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    CartaoCVV = table.Column<string>(type: "TEXT", maxLength: 5, nullable: false),
                    StatusPagamento = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    MotivoRejeicao = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamentos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_AlunoId_CursoId",
                table: "Pagamentos",
                columns: new[] { "AlunoId", "CursoId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pagamentos");
        }
    }
}
