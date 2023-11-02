using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace KanbanBoard.Api.Domain.Dtos
{
    public class CardDto
    {
        [Required(ErrorMessage = "Título is required")]
        [StringLength(80, ErrorMessage = "Título can't be longer than 80 characters")]
        [JsonProperty(PropertyName = "titulo")]
        public required string Titulo { get; set; }

        [JsonProperty(PropertyName = "conteudo")]
        public string? Conteudo { get; set; }

        [JsonProperty(PropertyName = "lista")]
        public string? Lista { get; set; }
    }
}