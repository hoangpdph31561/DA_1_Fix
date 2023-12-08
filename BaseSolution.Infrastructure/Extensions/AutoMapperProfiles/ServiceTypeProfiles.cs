using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Example;
using BaseSolution.Application.DataTransferObjects.ServiceType;
using BaseSolution.Application.DataTransferObjects.ServiceType.Request;
using BaseSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.Extensions.AutoMapperProfiles
{
    public class ServiceTypeProfiles : Profile
    {
        public ServiceTypeProfiles()
        {
            CreateMap<ServiceTypeEntity, ServiceTypeDto>()
                .ForMember(dest => dest.TotalServices,opt => opt.MapFrom(src => src.Services.Count(sv => !sv.Deleted)))
                .ForMember(dest => dest.TotalServiceOrders, opt => opt.MapFrom(src => src.Services.SelectMany(sv => sv.ServiceOrderDetails.Select(x => x.ServiceOrder.ServiceOrderDetails)).Count()));
            CreateMap<ServiceTypeCreateRequest, ServiceTypeEntity>();
            CreateMap<ServiceTypeUpDateRequest, ServiceTypeEntity>();
        }
    }
}
