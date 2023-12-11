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
                 .ForMember(des => des.RoomPrice, opt => opt.MapFrom(x => x.RoomBooking.RoomBookingDetails.Select(x => x.RoomDetail.Price).FirstOrDefault()))
                .ForMember(des => des.PrePaid, otp => otp.MapFrom(src => src.RoomBooking.RoomBookingDetails.Select(x => x.PrePaid).FirstOrDefault()))
                .ForMember(des => des.CheckInReality, otp => otp.MapFrom(src => src.RoomBooking.RoomBookingDetails.Select(x => x.CheckInReality).FirstOrDefault()))
                .ForMember(des => des.CheckOutReality, otp => otp.MapFrom(src => src.RoomBooking.RoomBookingDetails.Select(x => x.CheckOutReality).FirstOrDefault()))
                // service
                .ForMember(des => des.TotalServiceForRoom, otp => otp.MapFrom(src => src.RoomBooking.RoomBookingDetails.SelectMany(x => x.ServiceOrders).SelectMany(x => x.ServiceOrderDetails).Select(x => x.ServiceId).Count()))
                .ForMember(des => des.ServicePriceForRoom, otp => otp.MapFrom(src => src.RoomBooking.RoomBookingDetails.SelectMany(x => x.ServiceOrders).SelectMany(x => x.ServiceOrderDetails).Select(x => x.Service.Price).FirstOrDefault()))
                .ForMember(des => des.NameServiceForRoom, otp => otp.MapFrom(src => src.RoomBooking.RoomBookingDetails.SelectMany(x => x.ServiceOrders).SelectMany(x => x.ServiceOrderDetails).Select(x => x.Service.Name).FirstOrDefault()))
                 .ForMember(dest => dest.QuantityService, opt => opt.MapFrom(src => src.ServiceOrder.ServiceOrderDetails == null || !src.ServiceOrder.ServiceOrderDetails.Any() ? 0 : src.ServiceOrder.ServiceOrderDetails.Where(ard => !ard.Deleted).Sum(ard => ard.Amount)))
                .ForMember(des => des.PriceService, opt => opt.MapFrom(src => src.ServiceOrder.ServiceOrderDetails.Select(x => x.Price).FirstOrDefault()))
                 .ForMember(des => des.ServiceOrderId, opt => opt.MapFrom(src => src.ServiceOrder.Id));
        }
    }
}
