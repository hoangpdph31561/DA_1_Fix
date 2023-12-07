using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Customer;
using BaseSolution.Application.DataTransferObjects.Customer.Request;
using BaseSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.Extensions.AutoMapperProfiles
{
    public class CustomerProfiles : Profile
    {
        public CustomerProfiles()
        {
            CreateMap<CustomerEntity,CustomerDto>();
            CreateMap<CustomerCreateRequest, CustomerEntity>();
            CreateMap<CustomerUpdateRequest, CustomerEntity>();
            CreateMap<CustomerDeleteRequest, CustomerEntity>();
            CreateMap<CustomerDetailUpdateRequest, CustomerEntity>();
        }
    }
}
