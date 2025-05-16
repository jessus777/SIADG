using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SIADG.Persistence.Database;
using SIADG.Persistence.Enums;

namespace SIADG.Persistence.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceContextService(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var dbProvider = configuration["DatabaseSettings:Provider"] == "PostgreSQL"
       ? DatabaseProvider.PostgreSQL
       : DatabaseProvider.SqlServer;

        var connectionString = configuration.GetConnectionString("GymDatabaseConection")??string.Empty;

        services.AddDbContext<AppDbContext>(options =>
        {
            DbContextFactory.ConfigureDbContext(options, connectionString, dbProvider);
        });


        return services;
    }

}
