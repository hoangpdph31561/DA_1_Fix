using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Building;
using BaseSolution.Application.DataTransferObjects.Building.Request;
using BaseSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.Extensions.AutoMapperProfiles
{
    public class BuildingProfile : Profile
    {
        public BuildingProfile()
        {
            CreateMap<BuildingEntity, BuildingDTO>().ForMember(
                dest => dest.NumberOfFloors, opt => opt.MapFrom(src => src.Floors.Where(fl => !fl.Deleted).Count()));
            CreateMap<BuildingCreateRequest, BuildingEntity>();
            CreateMap<BuildingUpdateRequest, BuildingEntity>(); 
            CreateMap<BuildingCreateRequest, BuildingDTO>();
        }
    }
}
