using Contracts.Common.Interfaces;
using Whatsapp.Domain.Entities;

namespace Whatsapp.Application.Common.Interfaces;

public interface IMessageRepository : IRepositoryBase<Message, Guid>
{
    Task<IEnumerable<Message>> GetMessagesByChatIdAsync(Guid chatId); 
}