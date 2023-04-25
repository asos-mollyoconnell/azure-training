using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Customers.GetById;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Application.CanonicalCustomer.Queries.GetById;

namespace Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection service)
        {
            service.AddMediatR(typeof(GetCustomerByIdHandler));
            service.AddMediatR(typeof(GetCanonicalCustomerByIdHandler));

            return service;
        }
    }
}
