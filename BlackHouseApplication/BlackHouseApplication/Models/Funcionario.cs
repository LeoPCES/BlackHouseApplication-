using System.ComponentModel.DataAnnotations;

namespace BlackHouseApplication.Models
{
    public class Funcionario
    {
        public int FuncionarioId { get; set; }
        [Required(ErrorMessage = "Obrigatório")]
        [Display(Name = "Nome: ")]
        [StringLength(255)]
        public string FuncionarioNome { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "Obrigatório")]
        public string Telefone { get; set; }
     
    }
}
