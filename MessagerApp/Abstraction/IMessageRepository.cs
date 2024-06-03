using MessagerApp.Models;

namespace MessagerApp.Abstraction
{
    public interface IMessageRepository
    {
        IEnumerable<Message> GetMessageForUser(Guid userId);
        void SendMessage(Message message);
    }
}