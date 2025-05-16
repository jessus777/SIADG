using Microsoft.EntityFrameworkCore;

namespace SIADG.Persistence.Database;
public abstract  class AppDbContext
    : DbContext
{
    // Constructor acepta DbContextOptions<T> genérico
    protected AppDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuraciones comunes a todos los proveedores
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly
        );
    }


}
