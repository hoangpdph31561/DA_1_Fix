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
            CreateMap<RoomTypeEntity, RoomTypeDTO>();
            CreateMap<RoomTypeCreateRequest, RoomTypeEntity>();
            CreateMap<RoomTypeUpdateRequest, RoomTypeEntity>();
            CreateMap<RoomTypeCreateRequest, RoomTypeDTO>();
        }
    }
}
