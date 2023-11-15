using BlackHouseApplication.Context;
using BlackHouseApplication.Models;
using BlackHouseApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlackHouseApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class AdminConferirAgendamentosController : Controller
    {
        private readonly AppDbContext _context;

        public AdminConferirAgendamentosController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Lista(int? id)
        {
            IEnumerable<Agendamento> agendamentos;
            // Busca o funcionário no banco de dados
            var funcionario = _context.Funcionarios.FirstOrDefault(f => f.FuncionarioId == id);

            if (id == null)
            {
                // Se nenhum ID de barbeiro foi fornecido, mostrar uma lista de todos os barbeiros

                return RedirectToAction("Index", "AdminFuncionarios");
            }
            else
            {
                // Se um ID de barbeiro foi fornecido, mostrar os agendamentos para esse barbeiro
                agendamentos = _context.Agendamentos
               .Include(a => a.Servico)
               .Include(a => a.Funcionario)
               .Where(a => a.FuncionarioId == id && a.DataAgendamento > DateTime.Now)
               .OrderBy(a => a.DataAgendamento);

            }

            var funcionarioListaViewModel = new FuncionarioListaViewModel
            {
                Agendamentos = agendamentos,
                FuncionarioNome = funcionario.FuncionarioNome
            };

            return View(funcionarioListaViewModel);
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Agendamentos == null)
            {
                return Problem("Entidade da tabela 'AppDbContext.Servicos'  é nula.");
            }
            var agentamento = await _context.Agendamentos.FindAsync(id);
            if (agentamento != null)
            {
                _context.Agendamentos.Remove(agentamento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Lista), new { id = agentamento.FuncionarioId });
        }

    }
}
