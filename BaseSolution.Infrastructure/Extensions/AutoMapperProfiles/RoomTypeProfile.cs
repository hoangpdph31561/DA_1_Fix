using AutoMapper;
using BaseSolution.Application.DataTransferObjects.RoomType.Request;
using BaseSolution.Application.DataTransferObjects.RoomType;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Infrastructure.Extensions.AutoMapperProfiles
{
    public class RoomTypeProfile : Profile
    {
        public RoomTypeProfile()
        {
            CreateMap<RoomTypeEntity, RoomTypeDTO>()
                .ForMember(desc => desc.NumberOfAmenityRoomDetail, opt => opt.MapFrom(src => src.AmenityRoomDetails.Count()))
                .ForMember(desc => desc.AmountOfRoomType, opt => opt.MapFrom(src => src.RoomDetails.Count()));

            CreateMap<RoomTypeCreateRequest, RoomTypeEntity>();
            CreateMap<RoomTypeUpdateRequest, RoomTypeEntity>();
            CreateMap<RoomTypeDeleteRequest, RoomTypeEntity>();
        }
    }
}
