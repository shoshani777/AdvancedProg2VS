namespace ChatApi
{
    public class Invitation
    {
        public string? From { get; set; }
        public string? To { get; set; }
        public string? Server { get; set; }
    }
    public class Transfer
    {
        public string? From { get; set; }
        public string? To { get; set; }
        public string? Content { get; set; }
    }
    public class ContactForAdding
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Server { get; set; }

    }
    public class ContactForEditting
    {
        public int? unread { get; set; }
        public int? unreadMark { get; set; }
        public bool? isClicked { get; set; }

    }
    public class MessageContent
    {
        public string? Content { get; set; }
    }
}

