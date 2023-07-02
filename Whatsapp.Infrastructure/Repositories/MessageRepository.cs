using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Whatsapp.Application.Common.Interfaces;
using Whatsapp.Domain.Entities;
using Whatsapp.Infrastructure.Persistence;

namespace Whatsapp.Infrastructure.Repositories;

public class MessageRepository : RepositoryBase<Message, Guid, WhatsappDbContext>, IMessageRepository
{
   public MessageRepository(WhatsappDbContext dbContext, IUnitOfWork<WhatsappDbContext> unitOfWork) : base(dbContext, unitOfWork)
   {
   }


   public async Task<IEnumerable<Message>> GetMessagesByChatIdAsync(Guid chatId)
   {
      var result = await FindByCondition(x => x.ChatId == chatId).ToListAsync();
      return result;
   }
}