using CleanFirmeza.Domain.Entities;
using CleanFirmeza.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanFirmeza.Infrastructure.Persistence;


    /**
 * Procesa los datos (credenciales) para acceder a la base de datos, osea
 * el servidor de clever cloude
 *
 * Se utiliza el constructor primario (osea el constructor integrado dentro de la clase principal)
 * solo para tener mayor estetica y organización
 */
    public class ApplicationDbContext : IdentityDbContext<ApplicationUsser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        /*
         *
         * public TYPE Type { get; set; }
         */

        public DbSet<Product> Products { get; set; }

        public DbSet<Client> Clients { get; set; }
        // public DbSet<Order> Orders { get; set; }
        // public DbSet<OrderInfo> OrdersInfo { get; set; }
        //
    }