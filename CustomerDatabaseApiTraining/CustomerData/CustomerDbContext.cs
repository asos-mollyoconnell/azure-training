using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using CustomerData.Models;
using Microsoft.EntityFrameworkCore;


namespace CustomerData
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Contact> Contacts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 2,
                    Forename = "Molly",
                    Surname = "OConnell",
                    Email = "email@email.com",
                    DateOfBirth = DateTime.Parse("04/10/1998 12:11:00")
                });


            modelBuilder.Entity<Address>().HasData(
                new Address
                {
                    Id = 2,
                    CustomerId = 2,
                    Line1 = "line1",
                    Line2 = "line 2",
                    City = "city",
                    County = "county",
                    Country = "country",
                    Postcode = "postcode"
                });

            modelBuilder.Entity<Contact>().HasData(
                new Contact
                {
                    Id = 2,
                    CustomerId = 2,
                    HomeNumber = 1234567890,
                    MobileNumber = 0987654321
                });
        }

    }
}

