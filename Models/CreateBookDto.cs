using LibaryApi.Entities.Enum;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibaryApi.Models
{
    public class CreateBookDto
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Author { get; set; } = string.Empty;
        [MaxLength(250)]
        public string Description { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status BookStatus { get; set; } // This is the BookStatus enum
    }
}
