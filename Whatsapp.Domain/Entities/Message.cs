using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domains;
using Whatsapp.Domain.Enums;

namespace Whatsapp.Domain.Entities;

public class Message : EntityAuditBase<Guid>
{
    [Required]
    [Column(TypeName = "nvarchar(250)")]
    public string Content { get; set; } 
    
    public Guid ChatId { get; set; }
    
    public MessageStatus Status { get; set; }
}