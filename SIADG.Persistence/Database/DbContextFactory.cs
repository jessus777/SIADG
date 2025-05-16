using Microsoft.EntityFrameworkCore;
using SIADG.Persistence.Enums;

namespace SIADG.Persistence.Database;
public static class DbContextFactory
{
    // Método para DbContextOptionsBuilder<AppDbContext>
    public static DbContextOptionsBuilder<AppDbContext> ConfigureDbContext(
        DbContextOptionsBuilder<AppDbContext> builder,
        string connectionString,
        DatabaseProvider provider)
    {
        ConfigureDbContextInternal(builder, connectionString, provider);
        return builder;
    }

    // Método para DbContextOptionsBuilder genérico
    public static DbContextOptionsBuilder ConfigureDbContext(
        DbContextOptionsBuilder builder,
        string connectionString,
        DatabaseProvider provider)
    {
        ConfigureDbContextInternal(builder, connectionString, provider);
        return builder;
    }

    // Método interno que implementa la lógica de configuración
    private static void ConfigureDbContextInternal(
        DbContextOptionsBuilder builder,
        string connectionString,
        DatabaseProvider provider)
    {
        switch (provider)
        {
            case DatabaseProvider.SqlServer:
                builder.UseSqlServer(connectionString,
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                        sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    });
                break;

            case DatabaseProvider.PostgreSQL:
                builder.UseNpgsql(connectionString,
                    npgsqlOptions =>
                    {
                        npgsqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                        npgsqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    });
                break;

            default:
                throw new ArgumentException($"Proveedor de base de datos no soportado: {provider}");
        }
    }
}