using BlackHouseApplication.Models;

namespace BlackHouseApplication.Repositories.Interfaces
{
    public interface IFuncionarioRepository
    {
        IEnumerable<Funcionario> Funcionarios { get; } // propriedade somente leitura para retornar uma coleção de objetos Funcionarios
    }
}
