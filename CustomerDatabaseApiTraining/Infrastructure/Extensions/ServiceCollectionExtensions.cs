using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerData;
using Application.Customers.Contracts;
using Infrastructure.Mapper;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastucture(this IServiceCollection services)
        {
            var config = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            services
                .AddDbContext<CustomerDbContext>(options => options.UseSqlServer(config.GetConnectionString("CustomerDbConnection")))
                .AddTransient<ICustomerRepository, CustomerRepository>()
                .AddAutoMapper(typeof(CustomerModelMapper));
            return services;
        }
    }
}
