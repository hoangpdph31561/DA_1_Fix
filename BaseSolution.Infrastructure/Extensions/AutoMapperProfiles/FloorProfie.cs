using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Floor;
using BaseSolution.Application.DataTransferObjects.Floor.Request;
using BaseSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.Extensions.AutoMapperProfiles
{
    public class FloorProfie : Profile
    {
        public FloorProfie()
        {
            CreateMap<FloorEntity, FloorDTO>().ForMember(
                dest => dest.BuildingName, opt => opt.MapFrom(src => src.Building.Name))
                .ForMember(
                dest => dest.NumberOfRoomRent, opt => opt.MapFrom(src => src.RoomDetails.Where(rd => !rd.Deleted).Count()));
            CreateMap<FloorCreateRequest, FloorEntity>();
            CreateMap<FloorUpdateRequest, FloorEntity>();
            CreateMap<FloorCreateRequest, FloorDTO>();
        }
    }
}
