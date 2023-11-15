using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlackHouseApplication.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_Funcionarios_FuncionarioId",
                table: "Agendamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_Servico_ServicoId",
                table: "Agendamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_Solicitacao_SolicitacaoId",
                table: "Agendamento");

            migrationBuilder.DropTable(
                name: "Solicitacao");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Servico",
                table: "Servico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agendamento",
                table: "Agendamento");

            migrationBuilder.DropIndex(
                name: "IX_Agendamento_SolicitacaoId",
                table: "Agendamento");

            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "SolicitacaoId",
                table: "Agendamento");

            migrationBuilder.DropColumn(
                name: "ValorAgendamento",
                table: "Agendamento");

            migrationBuilder.RenameTable(
                name: "Servico",
                newName: "Servicos");

            migrationBuilder.RenameTable(
                name: "Agendamento",
                newName: "Agendamentos");

            migrationBuilder.RenameIndex(
                name: "IX_Agendamento_ServicoId",
                table: "Agendamentos",
                newName: "IX_Agendamentos_ServicoId");

            migrationBuilder.RenameIndex(
                name: "IX_Agendamento_FuncionarioId",
                table: "Agendamentos",
                newName: "IX_Agendamentos_FuncionarioId");

            migrationBuilder.AddColumn<string>(
                name: "ClienteNome",
                table: "Agendamentos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "HorarioAgendamento",
                table: "Agendamentos",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "TelefoneCliente",
                table: "Agendamentos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Servicos",
                table: "Servicos",
                column: "ServicoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agendamentos",
                table: "Agendamentos",
                column: "AgendamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Funcionarios_FuncionarioId",
                table: "Agendamentos",
                column: "FuncionarioId",
                principalTable: "Funcionarios",
                principalColumn: "FuncionarioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Servicos_ServicoId",
                table: "Agendamentos",
                column: "ServicoId",
                principalTable: "Servicos",
                principalColumn: "ServicoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Funcionarios_FuncionarioId",
                table: "Agendamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Servicos_ServicoId",
                table: "Agendamentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Servicos",
                table: "Servicos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agendamentos",
                table: "Agendamentos");

            migrationBuilder.DropColumn(
                name: "ClienteNome",
                table: "Agendamentos");

            migrationBuilder.DropColumn(
                name: "HorarioAgendamento",
                table: "Agendamentos");

            migrationBuilder.DropColumn(
                name: "TelefoneCliente",
                table: "Agendamentos");

            migrationBuilder.RenameTable(
                name: "Servicos",
                newName: "Servico");

            migrationBuilder.RenameTable(
                name: "Agendamentos",
                newName: "Agendamento");

            migrationBuilder.RenameIndex(
                name: "IX_Agendamentos_ServicoId",
                table: "Agendamento",
                newName: "IX_Agendamento_ServicoId");

            migrationBuilder.RenameIndex(
                name: "IX_Agendamentos_FuncionarioId",
                table: "Agendamento",
                newName: "IX_Agendamento_FuncionarioId");

            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "Funcionarios",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "Funcionarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SolicitacaoId",
                table: "Agendamento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorAgendamento",
                table: "Agendamento",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Servico",
                table: "Servico",
                column: "ServicoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agendamento",
                table: "Agendamento",
                column: "AgendamentoId");

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteNome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteId);
                });

            migrationBuilder.CreateTable(
                name: "Solicitacao",
                columns: table => new
                {
                    SolicitacaoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Horario = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Preco_Solicitacao = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitacao", x => x.SolicitacaoId);
                    table.ForeignKey(
                        name: "FK_Solicitacao_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_SolicitacaoId",
                table: "Agendamento",
                column: "SolicitacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacao_ClienteId",
                table: "Solicitacao",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamento_Funcionarios_FuncionarioId",
                table: "Agendamento",
                column: "FuncionarioId",
                principalTable: "Funcionarios",
                principalColumn: "FuncionarioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamento_Servico_ServicoId",
                table: "Agendamento",
                column: "ServicoId",
                principalTable: "Servico",
                principalColumn: "ServicoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamento_Solicitacao_SolicitacaoId",
                table: "Agendamento",
                column: "SolicitacaoId",
                principalTable: "Solicitacao",
                principalColumn: "SolicitacaoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
