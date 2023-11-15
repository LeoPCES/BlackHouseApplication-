using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlackHouseApplication.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoTabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HorarioAgendamento",
                table: "Agendamentos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "HorarioAgendamento",
                table: "Agendamentos",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
