using BlackHouseApplication.Context;
using BlackHouseApplication.Models;
using BlackHouseApplication.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlackHouseApplication.Repositories
{
    public class AgendamentoRepository: IAgendamentoRepository
    {
        private readonly AppDbContext _context;

        public AgendamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DateTime>> BuscarHorariosDisponiveisAsync(DateTime data, int funcionarioId)
        {
            // Verificar se a data é um domingo ou anterior à data atual
            if (data.DayOfWeek == DayOfWeek.Sunday || data.Date < DateTime.Today)
            {
                // Se for um domingo, retornar uma lista vazia
                return new List<DateTime>();
            }

            // Gerar todos os horários possíveis para o dia
            var horariosPossiveis = new List<DateTime>();

            for (var time = new TimeSpan(8, 0, 0); time <= new TimeSpan(19, 0, 0); time = time.Add(new TimeSpan(0, 30, 0)))
            {
                horariosPossiveis.Add(data.Date + time);
            }

            // Buscar os agendamentos para o barbeiro na data
            var agendamentosDoBarbeiro =  await _context.Agendamentos
                .Where(a => a.DataAgendamento.Date == data.Date && a.FuncionarioId == funcionarioId)
                .ToListAsync();

            // Remover os horários que já estão agendados
            var horariosDisponiveis = horariosPossiveis.Except(agendamentosDoBarbeiro.Select(a => a.DataAgendamento)).ToList();


            await _context.SaveChangesAsync();

            return horariosDisponiveis;
        }

    }
}
