using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ProjectAndTaskManagement.Infrastructure.Persistence
{
    /// <summary>
    /// Esto permite a EF Core generar migraciones incluso fuera de la app en ejecución
    /// usando la base de datos de Clever Cloud (PostgreSQL).
    /// </summary>
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // Connection string para PostgreSQL en Clever Cloud
            optionsBuilder.UseNpgsql(
                "Host=bl5bu715fpymaeu75ez1-postgresql.services.clever-cloud.com;" +
                "Port=50013;" +
                "Database=bl5bu715fpymaeu75ez1;" +
                "Username=ullvuiwqihynujc1ujj7;" +     
                "Password=SXZzSP3NAnHTwhjKlBcvgfCLCAcxBc;" +
                "Pooling=true;" +
                "SSL Mode=Require;" +
                "Trust Server Certificate=true"
            );

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}