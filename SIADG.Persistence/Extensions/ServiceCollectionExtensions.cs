using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SIADG.Persistence.Database;

namespace SIADG.Persistence.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceService(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var providerName = configuration["DatabaseProvider"];

            switch (providerName?.Trim())
            {
                case "Postgres":
                    options.UseNpgsql(connectionString, npgsqlOptions =>
                        npgsqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
                    break;

                case "SqlServer":
                    options.UseSqlServer(connectionString, sqlOptions =>
                        sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
                    break;

                default:
                    throw new NotSupportedException($"Proveedor de base de datos no soportado: {providerName}");
            }
        });

        return services;
    }
}
