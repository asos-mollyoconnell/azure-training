using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CanonicalCustomer.Contracts;
using CustomerData;
using Application.Customers.Contracts;
using CanonicalCustomerData;
using Infrastructure.CanonicalCustomer;
using Infrastructure.Mappers;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            var config = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            services
                .AddDbContext<CustomerDbContext>(options => options.UseSqlServer(config.GetConnectionString("CustomerDbConnection")))
                .AddDbContext<CanonicalCustomerDbContext>(options => options.UseSqlServer(config.GetConnectionString("CanonicalDbConnection")))
                .AddTransient<ICustomerRepository, CustomerRepository>()
                .AddTransient<ICanonicalCustomerRepository, CanonicalCustomerRepository>()
                .AddAutoMapper(typeof(CustomerModelMapper),typeof(CanonicalCustomerMapper));
            return services;
        }
    }
}
