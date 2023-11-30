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
                .ForMember(desc => desc.AmountOfRoomType, opt => opt.MapFrom(src => src.RoomDetails.Count()))
                .ForMember(dest => dest.NumberOfRoomDetails, opt => opt.MapFrom(src => src.RoomDetails.Count(rd => !rd.Deleted)))
                .ForMember(dest => dest.MaxPriceOfRoom, opt => opt.MapFrom(src => src.RoomDetails == null || !src.RoomDetails.Any() ? 0 : src.RoomDetails.Max(rd => rd.Price)))
                .ForMember(dest => dest.MinPriceOfRoom, opt => opt.MapFrom(src => src.RoomDetails == null || !src.RoomDetails.Any() ? 0 : src.RoomDetails.Min(rd => rd.Price)))
                .ForMember(dest => dest.NumberOfAmenities, opt => opt.MapFrom(src =>src.AmenityRoomDetails == null || !src.AmenityRoomDetails.Any() ? 0 : src.AmenityRoomDetails.Where(ard => !ard.Deleted).Sum(ard => ard.Amount)));
            CreateMap<RoomTypeCreateRequest, RoomTypeEntity>();
            CreateMap<RoomTypeUpdateRequest, RoomTypeEntity>();
            CreateMap<RoomTypeDeleteRequest, RoomTypeEntity>();
        }
    }
}
