using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Whatsapp.Domain.Entities;
using Whatsapp.Domain.Enums;

namespace Whatsapp.Infrastructure.EntityConfigurations;

public class MessageEntityConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder
            .Property(x => x.Status)
            .HasDefaultValue(MessageStatus.Sending)
            .IsRequired();
    }
}