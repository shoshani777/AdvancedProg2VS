using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApi
{
    public class Message
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string? Author { get; set; }
        public string? Content { get; set; }
        public DateTime Created { get; set; }
        [ForeignKey("Chat")]
        public int Chat { get; set; }
    }
}
