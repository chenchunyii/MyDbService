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
    }
}
