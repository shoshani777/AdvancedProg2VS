using System.ComponentModel.DataAnnotations;

namespace AdvancedProgramming2Server.Models
{
    public class User
    {
        [Key]
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? NickName { get; set; }

        [Required]
        public string? Passward { get; set; }

        [Required]
        public string? Server { get; set; }
    }
}
