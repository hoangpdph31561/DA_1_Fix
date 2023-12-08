using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Statistic.ServiceOrder;
using BaseSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.Extensions.AutoMapperProfiles
{
    public class ServiceOrderStatisticProfile : Profile
    {
        public ServiceOrderStatisticProfile()
        {
            CreateMap<ServiceOrderEntity, ServiceOrderStatisticDto>()
                .ForMember(x => x.Month, opt => opt.MapFrom(x => x.CreatedTime.Month))
                .ForMember(x => x.NameService, opt => opt.MapFrom(x => x.ServiceOrderDetails.Select(x => x.Service.Name).FirstOrDefault()));
        }
    }
}
