using Contracts.Domains.Interfaces;
using Microsoft.EntityFrameworkCore;
using Whatsapp.Domain.Entities;

namespace Whatsapp.Infrastructure.Persistence;

public class WhatsappDbContext : DbContext
{
    public DbSet<Message> Messages { get; set; }
    
    public WhatsappDbContext(DbContextOptions<WhatsappDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // It will apply all configurations from the assembly that contains the MessageEntityConfiguration class
        // No need to add each configuration manually
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WhatsappDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var modified = ChangeTracker
            .Entries()
            .Where(e => e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted);

        foreach (var item in modified)
        {
            switch (item.State)
            {
                case EntityState.Added:
                    if (item.Entity is IDateTracking addedEntity)
                    {
                        addedEntity.CreatedDate = DateTime.UtcNow;
                        item.State = EntityState.Added;
                    }
                    break;
                case EntityState.Modified:
                    Entry(item.Entity).Property("Id").IsModified = false;
                    if (item.Entity is IDateTracking modifiedEntity)
                    {
                        modifiedEntity.LastModifiedDate = DateTime.UtcNow;
                        item.State = EntityState.Modified;
                    }
                    break;
            }    
        }
        
        return base.SaveChangesAsync(cancellationToken);
    }
}