using System.ComponentModel.DataAnnotations;

namespace AdvancedProgramming2Server.Models
{
    public class Chat
    {
        [Key]
        [Required]
        public int ChatId { get; set; }

        public string? User1 { get; set; }

        public string? User2 { get; set; }
    }
}
