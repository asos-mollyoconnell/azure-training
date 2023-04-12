using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options){}

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Contact> Contacts { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Customer>().HasData(
        //        new Customer
        //        {
        //            Id = 1,
        //            Forename = "Molly",
        //            Surname = "O'Connell",
        //            Email = "email@email.com",
        //            DateOfBirth = DateTime.Parse("04/10/1998 12:11:00")
        //        });


        //    modelBuilder.Entity<Address>().HasData(
        //        new Address
        //        {
        //            Id = 1,
        //            CustomerId = 1,
        //            Line1 = "line1",
        //            Line2 = "line 2",
        //            City = "city",
        //            County = "county",
        //            Country = "country",
        //            Postcode = "postcode"
        //        });

        //    modelBuilder.Entity<Contact>().HasData(
        //        new Contact
        //        {
        //            Id = 1,
        //            CustomerId = 1,
        //            HomeNumber = 1234567890,
        //            MobileNumber = 0987654321
        //        });
        //}

    }
}

