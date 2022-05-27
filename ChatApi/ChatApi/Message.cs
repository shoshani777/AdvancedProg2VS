using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApi
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string? Author { get; set; }
        public string? Content { get; set; }
        public DateTime Created { get; set; }
<<<<<<< HEAD
        public int ChatId { get; set; }
=======
        public int Chat { get; set; }
>>>>>>> branch2
    }
}
