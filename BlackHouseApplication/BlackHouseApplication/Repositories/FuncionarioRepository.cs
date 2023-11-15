using BlackHouseApplication.Context;
using BlackHouseApplication.Models;
using BlackHouseApplication.Repositories.Interfaces;

namespace BlackHouseApplication.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly AppDbContext _context;

        public FuncionarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Funcionario> Funcionarios => _context.Funcionarios; // implementando a propriedade Funcionarios
    }
}
