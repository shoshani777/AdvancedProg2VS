using System.ComponentModel.DataAnnotations;

namespace ChatApi
{
    public class UserContact
    {
        [Key]
        public string? UserName { get; set; }
        public string? ContactOf { get; set; }
        public string? NickName { get; set; }
        public string? Server { get; set; }
    }
}
