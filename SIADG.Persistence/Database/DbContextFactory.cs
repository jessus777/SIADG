using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIADG.Persistence.DatabaseProviders;
using SIADG.Persistence.Repositories;

namespace SIADG.Persistence.Database;
public static class DbContextFactory
{
    public static AppDbContext Create(IConfiguration configuration)
    {
        var providers = new List<IDatabaseProvider>
        {
            //new PostgresDatabaseProvider(),
            new SqlServerDatabaseProvider(),
            // Agrega más proveedores aquí
        };

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var providerName = configuration["DatabaseProvider"]; // Ej: "Postgres" en appsettings.json

        var provider = providers.FirstOrDefault(p => p.IsFor(providerName ?? string.Empty))
            ?? throw new InvalidOperationException($"Proveedor de base de datos no soportado: {providerName}");

        var options = new DbContextOptionsBuilder<AppDbContext>();
        provider.Configure(options, connectionString ?? string.Empty);

        return new CustomDbContext(options.Options);
    }
}

// Implementación concreta
internal class CustomDbContext : AppDbContext
{
    public CustomDbContext(DbContextOptions options) : base(options) { }
}
