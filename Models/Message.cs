using System.ComponentModel.DataAnnotations;

namespace AdvancedProgramming2Server.Models
{
    public class Message
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public MessageType Type { get; set; } = MessageType.Text;

        public string? UserName { get; set; }

        public string? Body { get; set; }

        public DateTime SendingDate { get; set; }

        public int ChatId { get; set; }
    }
}
