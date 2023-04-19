using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;

namespace Infrastructure.Mappers
{
    public class CanonicalCustomerMapper : Profile
    {
        public CanonicalCustomerMapper()
        {
            CreateMap<CanonicalCustomerModel, CanonicalCustomerData.Models.CanonicalCustomer>().ReverseMap();
        }
    }
}
