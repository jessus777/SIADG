using Microsoft.EntityFrameworkCore;

namespace SIADG.Persistence.Repositories;
public interface IDatabaseProvider
{
    DbContextOptionsBuilder Configure(DbContextOptionsBuilder options, string connectionString);
    bool IsFor(string providerName); // "Postgres", "SqlServer", etc.
}
