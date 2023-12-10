using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Services;
using BaseSolution.Application.DataTransferObjects.Services.Request;
using BaseSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.Extensions.AutoMapperProfiles
{
    public class ServiceProfiles : Profile
    {
        public ServiceProfiles()
        {
            CreateMap<ServiceEntity,ServiceDTO>()
                .ForMember(dest => dest.ServiceTypeName, opt => opt.MapFrom(src => src.ServiceType.Name))
                .ForMember(dest => dest.TotalOrders, opt => opt.MapFrom(src => src.ServiceOrderDetails.Where(x => !x.Deleted).Select(sod => sod.Amount).Sum()));
            CreateMap<ServiceCreateRequest, ServiceEntity>();
            CreateMap<ServiceUpdateRequest, ServiceEntity>();
        }
    }
}
