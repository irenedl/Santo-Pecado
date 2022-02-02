using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace SantoPecado.Server
{
    public class BurgerStoreContext : ApiAuthorizationDbContext<BurgerStoreUser>
    {
        public BurgerStoreContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Burger> Burgers { get; set; }

        public DbSet<BurgerSpecial> Specials { get; set; }

        public DbSet<Topping> Toppings { get; set; }

        public DbSet<NotificationSubscription> NotificationSubscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring a many-to-many special -> topping relationship that is friendly for serialization
            modelBuilder.Entity<BurgerTopping>().HasKey(pst => new { pst.BurgerId, pst.ToppingId });
            modelBuilder.Entity<BurgerTopping>().HasOne<Burger>().WithMany(ps => ps.Toppings);
            modelBuilder.Entity<BurgerTopping>().HasOne(pst => pst.Topping).WithMany();

            // Inline the Lat-Long pairs in Order rather than having a FK to another table
            modelBuilder.Entity<Order>().OwnsOne(o => o.DeliveryLocation);
        }
    }
}