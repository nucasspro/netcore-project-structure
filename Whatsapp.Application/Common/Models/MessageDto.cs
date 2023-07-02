using Whatsapp.Application.Common.Mappings;
using Whatsapp.Domain.Entities;

namespace Whatsapp.Application.Common.Models;

public class MessageDto : IMapFrom<Message>
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public string Status { get; set; }
}