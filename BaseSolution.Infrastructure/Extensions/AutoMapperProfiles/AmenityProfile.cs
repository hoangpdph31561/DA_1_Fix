using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Amenity.Request;
using BaseSolution.Application.DataTransferObjects.Amenity;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Infrastructure.Extensions.AutoMapperProfiles
{
    public class AmenityProfile :Profile
    {
        public AmenityProfile()
        {
            CreateMap<AmenityEntity, AmenityDTO>().ForMember(
                dest => dest.Total, opt => opt.MapFrom(src => src.AmenityRoomDetails.Where(amr => !amr.Deleted).Select(amr => amr.Amount).Sum()))
                .ForMember(
                dest => dest.NumberOfRoomUse, opt => opt.MapFrom(src => src.AmenityRoomDetails.Where(amr => !amr.Deleted).Count(amr => amr.RoomType.RoomDetails.Any())));
            CreateMap<AmenityCreateRequest, AmenityEntity>();
            CreateMap<AmenityUpdateRequest, AmenityEntity>();
            CreateMap<AmenityCreateRequest, AmenityDTO>();
        }
    }
}
