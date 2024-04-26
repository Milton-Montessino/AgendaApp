using AgendaApp.API.Models;
using AgendaApp.API.Models.Get;
using AgendaApp.Data.Entities;
using AutoMapper;

namespace AgendaApp.API.Mappings
{
    public class ProfileMap : Profile
    {
        /// <summary>
        /// Método construtor da Classe.
        /// </summary>
        public ProfileMap()
        {
            //copiando dados da Model CRIAR REQUEST -> Entidade TAREFA
            CreateMap<CriarTarefaRequestModel, Tarefa>();

            //copiando dados da Entidade TAREFA -> Model CRIAR RESPONSE
            CreateMap<Tarefa, CriarTarefaResponseModel>();

            //copiando dados da Entidade TAREFA -> Model EDITAR RESPONSE
            CreateMap<Tarefa, EditarTarefaResponseModel>();

            //copiando dados da Entidade TAREFA -> Model EXCLUIR RESPONSE
            CreateMap<Tarefa, ExcluirTarefaResponseModel>();

            //copiando dados da Entidade TAREFA -> Model CONSULTAR RESPONSE
            CreateMap<Tarefa, ConsultarTarefaResponseModel>();
        }
    }
}
