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
            CreateMap<AmenityRoomDetailEntity, AmenityRoomDetailDTO>();
            CreateMap<AmenityRoomDetailCreateRequest, AmenityRoomDetailEntity>();
            CreateMap<AmenityRoomDetailUpdateRequest, AmenityRoomDetailEntity>();
            CreateMap<AmenityRoomDetailDeleteRequest, AmenityRoomDetailEntity>();
        }
    }
}
