using AutoMapper;
using BaseSolution.Application.DataTransferObjects.ServiceOrder;
using BaseSolution.Application.DataTransferObjects.ServiceOrder.Request;
using BaseSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.Extensions.AutoMapperProfiles
{
    public class ServiceOrderProfile : Profile
    {
        public ServiceOrderProfile()
        {
            CreateMap<ServiceOrderEntity, ServiceOrderDTO>().ForMember(
                dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name));
            CreateMap<ServiceOrderCreateRequest, ServiceOrderEntity>();
            CreateMap<ServiceOrderUpdateRequest, ServiceOrderEntity>();
        }
    }
}
