using Microsoft.EntityFrameworkCore;
using SIADG.Persistence.Database;
using SIADG.Persistence.Repositories;

namespace SIADG.Persistence.DatabaseProviders;
public class SqlServerDatabaseProvider
    : IDatabaseProvider
{
    public DbContextOptionsBuilder Configure(DbContextOptionsBuilder options, string connectionString)
    {
        return options.UseSqlServer(

            connectionString,
            b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
        );
    }

    public bool IsFor(string providerName)
    {
        return providerName.Equals("SqlServer", StringComparison.OrdinalIgnoreCase);
    }
}
