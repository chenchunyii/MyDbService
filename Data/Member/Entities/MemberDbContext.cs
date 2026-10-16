using Microsoft.EntityFrameworkCore;

namespace MyDbService.Data.Member.Entities
{
    public class MemberDbContext : DbContext
    {
        public MemberDbContext(DbContextOptions<MemberDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<FirstName> FirstNames { get; set; }
        public DbSet<LastName> LastNames { get; set; }
    }
}