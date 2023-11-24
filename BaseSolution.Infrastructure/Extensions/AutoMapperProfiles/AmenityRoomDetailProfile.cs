using AutoMapper;
using BaseSolution.Application.DataTransferObjects.AmenityRoomDetail.Request;
using BaseSolution.Application.DataTransferObjects.AmenityRoomDetail;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Infrastructure.Extensions.AutoMapperProfiles
{
    public class AmenityRoomDetailProfile : Profile
    {
        public AmenityRoomDetailProfile()
        {
            CreateMap<AmenityRoomDetailEntity, AmenityRoomDetailDTO>()
                .ForMember(desc => desc.NameOfAmenity, opt => opt.MapFrom(src => src.Amenity.Name));
            CreateMap<AmenityRoomDetailCreateRequest, AmenityRoomDetailEntity>();
            CreateMap<AmenityRoomDetailUpdateRequest, AmenityRoomDetailEntity>();
            CreateMap<AmenityRoomDetailDeleteRequest, AmenityRoomDetailEntity>();
        }
    }
}
