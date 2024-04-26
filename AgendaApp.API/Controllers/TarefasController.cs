using AgendaApp.API.Models;
using AgendaApp.API.Models.Get;
using AgendaApp.API.Models.Put;
using AgendaApp.Data.Entities;
using AgendaApp.Data.Entities.Enums;
using AgendaApp.Data.Repositories;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        //Atributo
        private readonly IMapper _mapper;

        /// <summary>
        /// Método Construtor para inicializar AutoMapper
        /// </summary>
        ///injeção de dependencia (inicialização  automatica)
        public TarefasController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Serviço da API para cadastro de tarefas
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CriarTarefaResponseModel), 201)]
        public IActionResult Post(CriarTarefaRequestModel model)
        {
            try
            {
                //copindo dados do objeto 'model' para 'tarefa'
                var tarefa = _mapper.Map<Tarefa>(model);
                tarefa.Id = Guid.NewGuid();
                tarefa.DataHoraCadastro = DateTime .Now;
                tarefa.DataHoraUltimaAtualizacao = DateTime .Now;
                tarefa.Status = 1;

                //Gravar no banco de dados
                var tarefaRepository = new TarefaRepository();
                tarefaRepository.Add(tarefa);

                //copindo dados do objeto  'tarefa' para 'model'
                var response = _mapper.Map<CriarTarefaResponseModel>(tarefa);

                return StatusCode(201, response);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { mensagem = "Ocorreu um ERRO! " + e.Message });
            }
        }

        /// <summary>
        /// Serviços para edição de tarefas.
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(EditarTarefaResponseModel), 200)]
        public IActionResult Put(EditarTarefaRequestModel model)
        {
            try
            {
                //buscar a tarefa no banco de dados através do ID
                var tarefaRepository = new TarefaRepository();
                var tarefa = tarefaRepository.GetById(model.Id.Value);

                //verificar se a tarefa foi encontrada
                if (tarefa != null)
                {
                    //modificar os dados da tarefa
                    tarefa.Nome = model.Nome;
                    tarefa.Descricao = model.Descricao;
                    tarefa.DataHora = model.DataHora;
                    tarefa.Prioridade = (PrioridadeTarefa)model.Prioridade;

                    //atualizar no banco de dados
                    tarefaRepository.Update(tarefa);

                    //atualizar o banco de dados
                    var response = _mapper.Map<EditarTarefaResponseModel>(tarefa);
                    return StatusCode(200, response);
                }
                else
                {
                    return StatusCode(400, new { message = "O Id da Tarefa é Invalido." });
                }

            }
            catch (Exception e)
            {
                return StatusCode(500, new { mensagem = "Ocorreu um ERRO! " + e.Message });
            }
        }

        /// <summary>
        /// Serviços para excluçao de tarefas.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ExcluirTarefaResponseModel), 200)]
        public IActionResult Delete(Guid id)
        {
            try
            {
                //buscar a tarefa no banco de dados através do ID
                var tarefaRepository = new TarefaRepository();
                var tarefa = tarefaRepository.GetById(id);

                //verificar se a tarefa foi encontrada
                if (tarefa != null)
                {
                    //excluindo a tarefa
                    tarefaRepository.Delete(tarefa);
                    

                    //copiando os dados do objeto 'tarefa' para o objeto 'response'
                    var response = _mapper.Map<ExcluirTarefaResponseModel>(tarefa);
                    response.DataHoraExclusao = DateTime.Now;

                    return StatusCode(200, response);
                }
                else
                {
                    return StatusCode(400, new { message = "O ID da tarefa é inválido." });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }

        }

        /// <summary>
        /// Serviço da API para consulta de tarefas
        /// </summary>
        [HttpGet("{dataInicio}/{dataFim}")]
        [ProducesResponseType(typeof(List<ConsultarTarefaResponseModel>), 200)]
        public IActionResult Get(DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                //consultar as tarefas no banco de dados através das datas
                var tarefaRepository = new TarefaRepository();
                var tarefas = tarefaRepository.Get(dataInicio, dataFim);

                //copiar os dados das tarefas para uma lista de ConsultarTarefaResponseModel
                var response = _mapper.Map<List<ConsultarTarefaResponseModel>>(tarefas);

                return StatusCode(200, response);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        /// <summary>
        /// Serviço da API para consultar 1 tarefa baseado no ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ConsultarTarefaResponseModel), 200)]
        public IActionResult GetById(Guid id)
        {
            try
            {
                //consultando a tarefa no banco de dados através do ID
                var tarefaRepository = new TarefaRepository();
                var tarefa = tarefaRepository.GetById(id);

                //verificar se a tarefa foi encontrada
                if (tarefa != null)
                {
                    //copiar os dados do objeto 'tarefa' para 'response'
                    var response = _mapper.Map<ConsultarTarefaResponseModel>(tarefa);

                    return StatusCode(200, response);
                }
                else
                {
                    return StatusCode(204); //Sucesso não encontrado
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

    }
}
