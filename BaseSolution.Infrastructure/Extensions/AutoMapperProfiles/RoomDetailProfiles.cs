using AutoMapper;
using BaseSolution.Application.DataTransferObjects.RoomDetail;
using BaseSolution.Application.DataTransferObjects.RoomDetail.Request;
using BaseSolution.Application.DataTransferObjects.RoomType.Request;
using BaseSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.Extensions.AutoMapperProfiles
{
    public class RoomDetailProfiles : Profile
    {
        public RoomDetailProfiles()
        {
            CreateMap<RoomDetailEntity,RoomDetailDto>()
                .ForMember(dest => dest.FloorName, opt => opt.MapFrom(src => src.Floor.Name))
                .ForMember(dest => dest.RoomTypeName, opt => opt.MapFrom(src => src.RoomType.Name))
                .ForMember(dest => dest.BuildingId, opt => opt.MapFrom(src => src.Floor.BuildingId))
                .ForMember(dest => dest.BuildingName, opt => opt.MapFrom(src => src.Floor.Building.Name))
                .ForMember(dest => dest.FloorStatus, opt => opt.MapFrom(src => src.Floor.Status))
                .ForMember(dest => dest.BuildingStatus, opt => opt.MapFrom(src => src.Floor.Building.Status))
                .ForMember(dest => dest.RoomTypeStatus, opt => opt.MapFrom(src => src.RoomType.Status));
            CreateMap<RoomDetailCreateRequest, RoomDetailEntity>();
            CreateMap<RoomDetailUpdateRequest, RoomDetailEntity>();
            CreateMap<RoomDetailDeleteRequest, RoomDetailEntity>();
        }
    }
}
