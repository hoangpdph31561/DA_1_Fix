using AutoMapper;
using BaseSolution.Application.DataTransferObjects.RoomBookingDetail;
using BaseSolution.Application.DataTransferObjects.RoomBookingDetail.Request;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Infrastructure.Extensions.AutoMapperProfiles
{
    public class RoomBookingDetailProfile : Profile
    {
        public RoomBookingDetailProfile()
        {
            CreateMap<RoomBookingDetailEntity, RoomBookingDetailDTO>();
                
            CreateMap<RoomBookingDetailCreateRequest, RoomBookingDetailEntity>();
            CreateMap<RoomBookingDetailUpdateRequest, RoomBookingDetailEntity>();
            CreateMap<RoomBookingDetailDeleteRequest, RoomBookingDetailEntity>();
        }
    }
}
