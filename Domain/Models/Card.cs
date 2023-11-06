using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KanbanBoard.Api.Domain.Models
{
    public class Card
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "titulo")]
        public required string Titulo { get; set; }

        [JsonProperty(PropertyName = "conteudo")]
        public required string Conteudo { get; set; }

        [JsonProperty(PropertyName = "lista")]
        public required string Lista { get; set; }
    }
}