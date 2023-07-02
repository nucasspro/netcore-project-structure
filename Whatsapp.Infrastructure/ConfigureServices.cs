using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Whatsapp.Application.Common.Interfaces;
using Whatsapp.Infrastructure.Persistence;
using Whatsapp.Infrastructure.Repositories;

namespace Whatsapp.Infrastructure;

public static class ConfigureServices
{ 
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WhatsappDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnectionString"),
                builder => builder.MigrationsAssembly(typeof(WhatsappDbContext).Assembly.FullName)
            );
        });

        services.AddScoped<WhatsappContextSeed>();

        services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        services.AddScoped<IMessageRepository, MessageRepository>();
        
        return services;
    }
}