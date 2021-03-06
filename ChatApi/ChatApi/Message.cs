using System.ComponentModel.DataAnnotations;

namespace ChatApi
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string? Author { get; set; }
        public string? Content { get; set; }
        public DateTime? Created { get; set; }
        public int Chat { get; set; }
    }
}
