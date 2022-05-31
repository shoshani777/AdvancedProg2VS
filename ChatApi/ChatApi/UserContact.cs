using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApi
{
    public class UserContact
    {
        public string? UserName { get; set; }
        public string? ContactOf { get; set; }
        public string? NickName { get; set; }
        public string? Server { get; set; }
        public bool isClicked { get; set; } = false;
        public int unread { get; set; } = 0;
        public int unreadMark { get; set; } = 0;
        public DateTime Created { get; set; }
    }
}
