using AutoMapper;
using BaseSolution.Domain.Entities;
using BaseSolution.Application.DataTransferObjects.Roombooking;
using BaseSolution.Application.DataTransferObjects.Roombooking.Request;

namespace BaseSolution.Infrastructure.Extensions.AutoMapperProfiles
{
    public class RoomBookingProfile : Profile
    {
        public RoomBookingProfile()
        {
            CreateMap<RoomBookingEntity, RoombookingDTO>();
            CreateMap<RoombookingCreateRequest, RoomBookingEntity>();
            CreateMap<RoombookingUpdateRequest, RoomBookingEntity>();
            CreateMap<RoombookingDeleteRequest, RoomBookingEntity>();
        }
    }
}
