using Common.Logging;
using Serilog;
using Whatsapp.Application;
using Whatsapp.Infrastructure;
using Whatsapp.Infrastructure.Persistence;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateSlimBuilder(args);
builder.Host.UseSerilog(Serilogger.Configure);

Log.Information("Start Whatsapp API up");

try
{
    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructure(builder.Configuration);
    
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    
    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    
    // Initialize and seed database
    using (var scope = app.Services.CreateScope())
    {
        var contextSeed = scope.ServiceProvider.GetRequiredService<WhatsappContextSeed>();
        await contextSeed.InitialiseAsync();
        await contextSeed.SeedAsync();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    var type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
        throw;
    
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down Whatsapp API completely"); 
    Log.CloseAndFlush();
}
