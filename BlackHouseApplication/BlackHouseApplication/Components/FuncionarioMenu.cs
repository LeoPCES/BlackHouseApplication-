using BlackHouseApplication.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlackHouseApplication.Components
{
    public class FuncionarioMenu : ViewComponent
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        public FuncionarioMenu(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public IViewComponentResult Invoke()
        {
            var funcionarios = _funcionarioRepository.Funcionarios.OrderBy(b => b.FuncionarioNome);
            return View(funcionarios); // lista de objeto que vai como model para a view default
        }
    }
}
