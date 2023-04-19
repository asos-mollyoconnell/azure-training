using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CustomerData.Models;
using Domain.Models;

namespace Infrastructure.Repositories
{
    public class CustomerModelMapper : Profile
    {
        public CustomerModelMapper()
        {
            CreateMap<CustomerModel, Customer>().ReverseMap();
            CreateMap<AddressModel, Address>().ReverseMap();
            CreateMap<ContactModel, Contact>().ReverseMap();
        }
    }
}
