using AutoMapper;
using BaseSolution.Application.DataTransferObjects.ServiceOrderDetail;
using BaseSolution.Application.DataTransferObjects.ServiceOrderDetail.Request;
using BaseSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.Extensions.AutoMapperProfiles
{
    public class ServiceOrderDetailProfile : Profile
    {
        public ServiceOrderDetailProfile()
        {
            CreateMap<ServiceOrderDetailEntity, ServiceOrderDetailDTO>()
                .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service.Name))
                .ForMember(dest => dest.ServiceUnitType, opt => opt.MapFrom(src => src.Service.Unit));
            CreateMap<ServiceOrderDetailCreateRequest, ServiceOrderDetailEntity>();
            CreateMap<ServiceOrderDetailUpdateRequest, ServiceOrderDetailEntity>();
            CreateMap<ServiceOrderCreateUpdateDeleteRequest, ServiceOrderDetailEntity>();
        }
    }
}
