using Microsoft.EntityFrameworkCore;

namespace MyDbService.Data.FlashSale.Entities
{
    public class FlashSaleDbContext : DbContext
    {
        public FlashSaleDbContext(DbContextOptions<FlashSaleDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<FirstName> FirstNames { get; set; }
        public DbSet<LastName> LastNames { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>(e =>
            {
                e.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
                e.Property(x => x.UpdatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)")
                    .ValueGeneratedOnAddOrUpdate();
                e.Property(x => x.Price).HasPrecision(10, 2);
                e.ToTable(
                    t =>
                    {
                        t.HasCheckConstraint("CK_products_stock", "stock >= 0");
                        t.HasCheckConstraint("CK_products_price", "price >= 0");
                        t.HasCheckConstraint("CK_products_sale_start_at_and_sale_end_at", "sale_end_at > sale_start_at");
                    }
                );
            });

            modelBuilder.Entity<Order>(e =>
            {
                e.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
                e.Property(x => x.UpdatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)")
                    .ValueGeneratedOnAddOrUpdate();
                e.Property(x => x.UnitPrice).HasPrecision(10, 2);
                e.Property(x => x.Status)
                    .HasConversion<string>()
                    .HasMaxLength(20);
                e.ToTable(
                    t =>
                    {
                        t.HasCheckConstraint("CK_orders_quantity", "quantity > 0");
                        t.HasCheckConstraint("CK_orders_price", "price >= 0");
                    }
                );
            });
        }
    }
}
