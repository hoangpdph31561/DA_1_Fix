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
            CreateMap<ServiceEntity,ServiceDto>();
            CreateMap<ServiceCreateRequest, ServiceEntity>();
            CreateMap<ServiceUpdateRequest, ServiceEntity>();
        }
    }
}
