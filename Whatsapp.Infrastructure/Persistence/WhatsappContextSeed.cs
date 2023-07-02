using Contracts.Domains.Interfaces;
using Microsoft.EntityFrameworkCore;
using Whatsapp.Domain.Entities;
using Serilog;

namespace Whatsapp.Infrastructure.Persistence;

public class WhatsappContextSeed
{
    private readonly ILogger _logger;
    private readonly WhatsappDbContext _context;

    public WhatsappContextSeed(ILogger logger, WhatsappDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync(); 
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "An error occurred while seeding the database");
            throw;
        } 
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "An error occurred while seeding the database");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        if (!_context.Messages.Any())
        {
            var messages = new List<Message>
            {
                new(){Content = "Test message 1" },
                new(){Content = "Test message 2" },
                new(){Content = "Test message 3" },
                new(){Content = "Test message 4" },
                new(){Content = "Test message 5" },
            };
            
            await _context.Messages.AddRangeAsync(messages);
        }  
    }
}