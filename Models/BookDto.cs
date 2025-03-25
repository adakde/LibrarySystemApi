using LibaryApi.Entities.Enum;
using System.ComponentModel.DataAnnotations;

namespace LibaryApi.Models
{
    public class BookDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Author { get; set; } = string.Empty;
        [MaxLength(250)]
        public string Description { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public Status BookStatus { get; set; } // This is the BookStatus enum
    }
}
