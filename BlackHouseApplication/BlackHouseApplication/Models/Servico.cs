using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlackHouseApplication.Models
{
    public class Servico
    {
        [Key]
        public int ServicoId { get; set; }

        [Required(ErrorMessage = "O Tipo de Serviço deve ser informado")]
        [Display(Name = "Tipo do serviço")]
        [MinLength(1, ErrorMessage = "O serviço deve ter no mínimo {1} Caracteres")]
        public string TipoServico { get; set; }

        [Required(ErrorMessage = "Informe o valor do serviço")]
        [Display(Name = "Valor")]
        [Column(TypeName = "decimal(10, 2)")]
        [Range(1, 999.99, ErrorMessage = "O preco deve estar entre 1 e 999,99")]
        public decimal PrecoServico { get; set; }

        // usado para preencher a lista suspensa da View SelecionarBarbeiroServiço
        [NotMapped] // Este campo não será mapeado para o banco de dados
        public string TipoServicoComPreco
        {
            get
            {
                return $"{TipoServico} - R$ {PrecoServico}";
            }
        }

    }
}
