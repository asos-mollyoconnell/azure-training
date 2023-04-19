using CanonicalCustomerData.Models;
using Microsoft.EntityFrameworkCore;

namespace CanonicalCustomerData
{
    public class CanonicalCustomerDbContext : DbContext
    {
        public CanonicalCustomerDbContext(DbContextOptions<CanonicalCustomerDbContext> options) : base(options) { }


        public DbSet<CanonicalCustomer> CanonicalCustomers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CanonicalCustomer>().HasData(
                new CanonicalCustomer
                {
                    Id = 1,
                    FullName = "Molly OConnell",
                    Email = "email@email.com",
                    Country = "Ireland",
                    CustomerId = 1,
                    MobileNumber = 1234567890
                });
        }
    }
}
