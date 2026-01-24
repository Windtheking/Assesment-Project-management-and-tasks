using Microsoft.EntityFrameworkCore;
using ProjectAndTaskManagement.Domain.Entities;

namespace ProjectAndTaskManagement.Infrastructure.Persistence
{
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
            // Configuración de Project
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(p => p.Id);

                // Mapear Guid a uuid en PostgreSQL
                entity.Property(p => p.Id)
                      .HasColumnType("uuid");

                entity.Property(p => p.Name).IsRequired();
                entity.Property(p => p.Description).IsRequired();
                entity.Property(p => p.Status).HasDefaultValue("Draft");
                entity.Property(p => p.Priority).HasDefaultValue("Medium");

                entity.HasMany(p => p.TaskItems)
                      .WithOne(t => t.Project)
                      .HasForeignKey(t => t.ProjectId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de TaskItem
            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.HasKey(t => t.Id);

                // Mapear Guid a uuid en PostgreSQL
                entity.Property(t => t.Id)
                      .HasColumnType("uuid");

                entity.Property(t => t.Title).IsRequired();
                entity.Property(t => t.Priority).HasDefaultValue("Low");
                entity.Property(t => t.Order).IsRequired();
                entity.Property(t => t.IsCompleted).HasDefaultValue(false);

                // Asegurar que Order sea único por proyecto
                entity.HasIndex(t => new { t.ProjectId, t.Order }).IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
