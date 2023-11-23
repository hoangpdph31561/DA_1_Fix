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
                .ForMember(dest => dest.NumberOfRoomDetails, opt=> opt.MapFrom(src => src.RoomDetails.Count(rd => !rd.Deleted)));
            CreateMap<RoomTypeCreateRequest, RoomTypeEntity>();
            CreateMap<RoomTypeUpdateRequest, RoomTypeEntity>();
            CreateMap<RoomTypeDeleteRequest, RoomTypeEntity>();
        }
    }
}
