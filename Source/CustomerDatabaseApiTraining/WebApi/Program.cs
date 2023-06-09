using Application.Extensions;
using CustomerData;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CustomerWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddInfrastructure();

            builder.Services.AddApplication();


            //builder.Services.AddDbContext<CustomerDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            //builder.Services.AddDbContext<CanonicalCustomerDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerCanonicalDb")));

            //builder.Services.AddSingleton<Customer>();
            //builder.Services.AddSingleton<Address>();
            //builder.Services.AddSingleton<Contact>();
            //builder.Services.AddSingleton<CanonicalCustomers>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}