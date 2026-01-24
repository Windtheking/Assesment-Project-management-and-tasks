using Microsoft.EntityFrameworkCore;
using ProjectAndTaskManagement.Domain.Entities;


namespace ProjectAndTaskManagement.Infrastructure.Persistence;
/**
* Procesa los datos (credenciales) para acceder a la base de datos, osea
* el servidor de clever cloude
*
* Se utiliza el constructor primario (osea el constructor integrado dentro de la clase principal)
* solo para tener mayor estetica y organización
*/
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Project> Projects => Set<Project>();
    public DbSet<TaskItem> TaskItems => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>()
            .HasMany(p => p.TaskItems)
            .WithOne(t => t.Project)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}

