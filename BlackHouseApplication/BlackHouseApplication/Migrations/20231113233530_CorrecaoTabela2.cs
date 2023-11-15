using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlackHouseApplication.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoTabela2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HorariosDisponiveis");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HorariosDisponiveis",
                columns: table => new
                {
                    HorarioDisponivelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FuncionarioId = table.Column<int>(type: "int", nullable: false),
                    Horario = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorariosDisponiveis", x => x.HorarioDisponivelId);
                    table.ForeignKey(
                        name: "FK_HorariosDisponiveis_Funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionarios",
                        principalColumn: "FuncionarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HorariosDisponiveis_FuncionarioId",
                table: "HorariosDisponiveis",
                column: "FuncionarioId");
        }
    }
}
