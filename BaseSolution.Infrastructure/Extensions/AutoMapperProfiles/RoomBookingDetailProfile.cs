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
            CreateMap<RoomBookingDetailEntity, RoomBookingDetailDTO>()
                    .ForMember(des => des.NameCustomer,opt => opt.MapFrom(src => src.RoomBooking.Customer.Name));

            CreateMap<RoomBookingDetailCreateRequest, RoomBookingDetailEntity>();
            CreateMap<RoomBookingDetailUpdateRequest, RoomBookingDetailEntity>();
            CreateMap<RoomBookingDetailDeleteRequest, RoomBookingDetailEntity>();
        }
    }
}
