using BlackHouseApplication.Models;

namespace BlackHouseApplication.ViewModels
{
    public class FuncionarioListaViewModel
    {
        public IEnumerable<Agendamento> Agendamentos { get; set; }
        public string FuncionarioNome { get; set; }
    }
}
