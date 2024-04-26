namespace AgendaApp.API.Models.Get
{
    /// <summary>
    /// Modelo de dados da resposta de consultar de tarefa.
    /// </summary>
    public class ConsultarTarefaResponseModel
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataHora { get; set; }
        public int? Prioridade { get; set; }
        public DateTime? DataHoraCadastro { get; set; }
        public DateTime? DataHoraUltimaAtualizacao { get; set; }
        public int? Status { get; set; }
    }
}
