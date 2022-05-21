using System.ComponentModel.DataAnnotations;

namespace ChatApi
{
    public class User
    {
        [Key]
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? NickName { get; set; }
        public string? Server { get; set; }
    }
}
