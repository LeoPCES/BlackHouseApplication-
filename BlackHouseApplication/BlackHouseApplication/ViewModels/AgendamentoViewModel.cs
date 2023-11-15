using BlackHouseApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace BlackHouseApplication.ViewModels
{
    public class AgendamentoViewModel
    {
  
        public DateTime DataSelecionada { get; set; }

        public string ClienteNome { get; set; }
        public string TelefoneCliente { get; set; }


        public int ServicoSelecionadoId { get; set; }
        public int BarbeiroSelecionadoId { get; set; }

        //usado para ser exibida na lista suspensa
        public List<Servico> Servicos { get; set; }
        public List<Funcionario> Barbeiros { get; set; }
        public List<DateTime> HorariosDisponiveis { get; set; }

        public DateTime HorarioSelecionado { get; set; }
    }


}
