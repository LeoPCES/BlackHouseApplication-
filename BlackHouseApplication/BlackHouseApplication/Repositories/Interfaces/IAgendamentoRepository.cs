using BlackHouseApplication.Models;

namespace BlackHouseApplication.Repositories.Interfaces
{
    public interface IAgendamentoRepository
    {
        Task<List<DateTime>> BuscarHorariosDisponiveisAsync(DateTime data, int funcionarioId);

    }
}
