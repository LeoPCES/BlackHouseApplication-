using BlackHouseApplication.Context;
using BlackHouseApplication.Models;
using BlackHouseApplication.Repositories.Interfaces;
using BlackHouseApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace BlackHouseApplication.Controllers
{
    public class AgendaController : Controller
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly AppDbContext _context;

        public AgendaController(AppDbContext context, IAgendamentoRepository agendamentoRepository)
        {
            _agendamentoRepository = agendamentoRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> SelecionarBarbeiroServico()
        {
            var model = new AgendamentoViewModel
            {
                //inicialização do dia de hoje para a View
                DataSelecionada = DateTime.Today,
                // Buscar a lista de barbeiros e serviços do banco de dados para lista suspensa na view
                Barbeiros = await _context.Funcionarios.ToListAsync(),
                Servicos = await _context.Servicos.ToListAsync()
            };
             return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SelecionarBarbeiroServico(AgendamentoViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Buscar os horários disponíveis para o barbeiro selecionado na data selecionada
                
                var horariosDisponiveis = await _agendamentoRepository.BuscarHorariosDisponiveisAsync(model.DataSelecionada, model.BarbeiroSelecionadoId);

                // Verificar se há horários disponíveis
                if (horariosDisponiveis.Count == 0 || !horariosDisponiveis.Any())
                {
                    // Se não houver horários disponíveis, redirecionar para a página "SemHorarios"                    
                    return RedirectToAction("SemHorarios", new { data = model.DataSelecionada });
                }
                else
                {   
                    // caso aja horários disponíveis, redirecionar para página de confirmação de agendamento
                    return RedirectToAction("ConfirmarAgendamento", model); 
                }
            }

            // caso o ModelState seja inválido, retornar a própriaView com os atributos da ViewModel preenchidos           
            model.DataSelecionada = DateTime.Today;
            model.Barbeiros = await _context.Funcionarios.ToListAsync();
            model.Servicos = await _context.Servicos.ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmarAgendamento(AgendamentoViewModel model)
        {    
            model.HorariosDisponiveis = await _agendamentoRepository.BuscarHorariosDisponiveisAsync(model.DataSelecionada, model.BarbeiroSelecionadoId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarAgendamentoPost(AgendamentoViewModel model) // async
        {
            string padrao = @"[^\d]+";
            string telefone = Regex.Replace(model.TelefoneCliente, padrao, "");

            if (ModelState.IsValid)
            {
                // Código para salvar o agendamento no banco de dados
                // Criando um novo objeto Agendamento a partir dos dados da ViewModel
                var agendamento = new Agendamento
                {
                    DataAgendamento = model.HorarioSelecionado,
                    ClienteNome = model.ClienteNome,
                    TelefoneCliente = telefone,
                    ServicoId = model.ServicoSelecionadoId,
                    FuncionarioId = model.BarbeiroSelecionadoId
                };

                // Adiciona o novo agendamento ao contexto do banco de dados
                _context.Agendamentos.Add(agendamento);

                // Salva as alterações no banco de dados
               await _context.SaveChangesAsync();
               
                // Redireciona o usuário para uma página de confirmação ou outra página conforme necessário
                return RedirectToAction("AgendamentoConfirmado", new { id = agendamento.AgendamentoId });
            }

            // caso o ModelState seja inválido, retornar a View SemHorarios          
            return RedirectToAction("SemHorarios", model.DataSelecionada);
        }


        public async Task<IActionResult> AgendamentoConfirmado(int id)
        {
            // Busca o agendamento no banco de dados
            var agendamento = await _context.Agendamentos
                .Include(a => a.Servico)
                .Include(a => a.Funcionario)
                .FirstOrDefaultAsync(a => a.AgendamentoId == id);

            // Retorna a View com os detalhes do agendamento
            return View(agendamento);
        }

        public IActionResult SemHorarios(DateTime data)
        {
            // Retorna a View com a data para a qual não há horários disponíveis
            return View(data);
        }
    }
}