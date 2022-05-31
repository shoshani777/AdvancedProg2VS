namespace ChatApi.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(Message? message, string forUser);
    }
}