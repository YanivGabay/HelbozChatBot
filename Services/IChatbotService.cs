namespace HelbozChatBot.Services
{
    public interface IChatbotService
    {
        string ProcessIncomingMessage(string from, string body);
    }
}
