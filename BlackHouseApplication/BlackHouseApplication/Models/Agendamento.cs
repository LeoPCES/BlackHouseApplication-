using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlackHouseApplication.Models
{
    public class Agendamento
    {
        [Key]
        public int AgendamentoId { get; set; }
        public DateTime DataAgendamento { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        [Display(Name = "Nome: ")]
        [StringLength(255)]
        public string ClienteNome { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "Obrigatório")]
        public string TelefoneCliente { get; set; }

        //propriedades de navegacao
        public int ServicoId { get; set; }
        public virtual Servico Servico { get; set; } // relacionamento

        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        // define um relacionamento um para muitos e gera chaves estrangeiras:
        // Servico(not Null) e Funcionario(not Null)


    }
}
