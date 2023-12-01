using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Statistic.Bill;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Infrastructure.Extensions.AutoMapperProfiles
{
    public class BillStatisticProfile : Profile
    {
        public BillStatisticProfile()
        {
            CreateMap<BillEntity, BillStatisticDto>()
                .ForMember(des => des.Month, opt => opt.MapFrom(src => src.CreatedTime.Month))
                .ForMember(des => des.TotalService, opt => opt.MapFrom(src => src.Customer.ServiceOrders.Select(x => x.ServiceOrderDetails).Count()))
                .ForMember(des => des.ServicePrice, opt => opt.MapFrom(src => src.Customer.ServiceOrders.SelectMany(x => x.ServiceOrderDetails).Select(x => x.Service.Price).FirstOrDefault()))
                .ForMember(des => des.RoomPrice, opt => opt.MapFrom(src => src.Customer.RoomBookings.SelectMany(x => x.RoomBookingDetails).Select(x => x.RoomDetail.Price).FirstOrDefault()))
                .ForMember(des => des.TotalRoom, opt => opt.MapFrom(src => src.Customer.RoomBookings.Select(x => x.RoomBookingDetails).Count()));
        }
    }
}
