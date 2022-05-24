using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApi
{
    public class Message
    {
        public int Id { get; set; }
        public string? Author { get; set; }
        public string? Content { get; set; }
        public DateTime Created { get; set; }
        public int ChatId { get; set; }
    }
}
