using System.ComponentModel.DataAnnotations;

namespace AgendaApp.API.Models
{
    /// <summary>
    /// Modelo de dados da requisição de cadastro de tarefa
    /// </summary>
    public class CriarTarefaRequestModel
    {

        [Required (ErrorMessage = "Por favor, informe o {0} da tarefa.")]
        [MinLength (8, ErrorMessage ="Por favor, informe um {0} com pelo menos {1} caracteres")]
        [MaxLength(100, ErrorMessage = "Por favor, informe um {0} com no máximo {1} caracteres")]
        public string? Nome { get; set; }

        [MinLength(8, ErrorMessage = "Por favor, informe um {0} com pelo menos {1} caracteres")]
        [MaxLength(100, ErrorMessage = "Por favor, informe um {0} com no máximo {1} caracteres")]
        [Required (ErrorMessage = "Por favor, informe a {0} da tarefa.")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Por favor, informe a Data da tarefa.")]
        public DateTime? DataHora { get; set; }

        [Required(ErrorMessage = "Por favor, informe a {0} da tarefa.")]
        public int? Prioridade { get; set; }
    }
}
