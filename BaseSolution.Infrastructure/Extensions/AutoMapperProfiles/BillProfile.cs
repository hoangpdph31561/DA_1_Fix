using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Bill;
using BaseSolution.Application.DataTransferObjects.Bill.Request;
using BaseSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.Extensions.AutoMapperProfiles
{
    public class BillProfile : Profile
    {
        public BillProfile()
        {
            CreateMap<BillEntity, BillDTO>()
                // lấy ra tổng số lượng dịch vụ của phòng đó
                .ForMember(des => des.TotalService, opt => opt.MapFrom(src => src.RoomBooking.RoomBookingDetails.Select(x => x.ServiceOrders).Count()))
                // lấy ra giá của từng dịch vụ mà phòng đó sử dụng 
                .ForMember(des => des.ServicePrice, opt => opt.MapFrom(src => src.RoomBooking.RoomBookingDetails.SelectMany(x => x.ServiceOrders).SelectMany(x => x.ServiceOrderDetails).Select(x => x.Service.Price).FirstOrDefault()))
                // lấy ra giá phòng của khách hàng đó 
                .ForMember(des => des.RoomPrice, opt => opt.MapFrom(src => src.Customer.RoomBookings.SelectMany(x => x.RoomBookingDetails).Select(x => x.RoomDetail.Price).FirstOrDefault()))
                .ForMember(des => des.RoomName, opt => opt.MapFrom(x => x.RoomBooking.RoomBookingDetails.Select(x => x.RoomDetail.Name).FirstOrDefault()))
                .ForMember(des => des.NameService, otp => otp.MapFrom(src => src.RoomBooking.RoomBookingDetails.SelectMany(x => x.ServiceOrders).SelectMany(x => x.ServiceOrderDetails).Select(x => x.Service.Name).FirstOrDefault()))
                .ForMember(des => des.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
                .ForMember(des => des.PrePaid, otp => otp.MapFrom(src => src.RoomBooking.RoomBookingDetails.Select(x => x.PrePaid).FirstOrDefault()))
                .ForMember(des => des.CheckInReality, otp => otp.MapFrom(src => src.RoomBooking.RoomBookingDetails.Select(x => x.CheckInReality).FirstOrDefault()))
                .ForMember(des => des.CheckOutReality, otp => otp.MapFrom(src => src.RoomBooking.RoomBookingDetails.Select(x => x.CheckOutReality).FirstOrDefault()))
                .ForMember(des => des.TotalService, otp => otp.MapFrom(src => src.RoomBooking.RoomBookingDetails.SelectMany(x => x.ServiceOrders).SelectMany(x => x.ServiceOrderDetails).Select(x => x.ServiceId).Count()))
                .ForMember(des => des.ServicePrice, otp => otp.MapFrom(src => src.RoomBooking.RoomBookingDetails.SelectMany(x => x.ServiceOrders).SelectMany(x => x.ServiceOrderDetails).Select(x => x.Service.Price).FirstOrDefault()))
                .ForMember(des => des.NameService, otp => otp.MapFrom(src => src.RoomBooking.RoomBookingDetails.SelectMany(x => x.ServiceOrders).SelectMany(x => x.ServiceOrderDetails).Select(x => x.Service.Name).FirstOrDefault()))
                .ForMember(des => des.ServiceOrderId, otp => otp.MapFrom(src => src.RoomBooking.RoomBookingDetails.SelectMany(x => x.ServiceOrders.Select(x => x.Id)).FirstOrDefault()))
                ;

            CreateMap<BillCreateRequest, BillEntity>();
            CreateMap<BillUpdateRequest, BillEntity>();

            CreateMap<BillEntity, BillDtoForRoom>()
                .ForMember(des => des.CustomerName, opt => opt.MapFrom(x => x.Customer.Name))
                .ForMember(des => des.RoomName, opt => opt.MapFrom(x => x.RoomBooking.RoomBookingDetails.Select(x => x.RoomDetail.Name).FirstOrDefault()))
                .ForMember(des => des.StatusRoom, opt => opt.MapFrom(x => x.RoomBooking.RoomBookingDetails.Select(x => x.RoomDetail.Status).FirstOrDefault()))
                .ForMember(des => des.StatusRoomBooking, opt => opt.MapFrom(x => x.RoomBooking.Status))
                .ForMember(des => des.RoomPrice, opt => opt.MapFrom(x => x.RoomBooking.RoomBookingDetails.Select(x => x.RoomDetail.Price).FirstOrDefault()))
                .ForMember(des => des.PrePaid, otp => otp.MapFrom(src => src.RoomBooking.RoomBookingDetails.Select(x => x.PrePaid).FirstOrDefault()))
                .ForMember(des => des.CheckInReality, otp => otp.MapFrom(src => src.RoomBooking.RoomBookingDetails.Select(x => x.CheckInReality).FirstOrDefault()))
                .ForMember(des => des.CheckOutReality, otp => otp.MapFrom(src => src.RoomBooking.RoomBookingDetails.Select(x => x.CheckOutReality).FirstOrDefault()))
                // service
                .ForMember(des => des.TotalService, otp => otp.MapFrom(src => src.RoomBooking.RoomBookingDetails.SelectMany(x => x.ServiceOrders).SelectMany(x => x.ServiceOrderDetails).Select(x => x.ServiceId).Count()))
                .ForMember(des => des.ServicePrice, otp => otp.MapFrom(src => src.RoomBooking.RoomBookingDetails.SelectMany(x => x.ServiceOrders).SelectMany(x => x.ServiceOrderDetails).Select(x => x.Service.Price).FirstOrDefault()))
                .ForMember(des => des.NameService, otp => otp.MapFrom(src => src.RoomBooking.RoomBookingDetails.SelectMany(x => x.ServiceOrders).SelectMany(x => x.ServiceOrderDetails).Select(x => x.Service.Name).FirstOrDefault()))
                .ForMember(des => des.ServiceOrderId, otp => otp.MapFrom(src => src.RoomBooking.RoomBookingDetails.SelectMany(x => x.ServiceOrders.Select(x => x.Id)).FirstOrDefault()))
            ;

            CreateMap<BillEntity, BillDtoForService>()
                 .ForMember(des => des.CustomerName, opt => opt.MapFrom(x => x.Customer.Name))
                 .ForMember(dest => dest.ServiceId, opt => opt.MapFrom(src => src.ServiceOrder.ServiceOrderDetails.Select(x => x.ServiceId).FirstOrDefault()))
                .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.ServiceOrder.ServiceOrderDetails.Select(x => x.Service.Name).FirstOrDefault()))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.ServiceOrder.ServiceOrderDetails == null || !src.ServiceOrder.ServiceOrderDetails.Any() ? 0 : src.ServiceOrder.ServiceOrderDetails.Where(ard => !ard.Deleted).Sum(ard => ard.Amount)))
                .ForMember(des => des.Price, opt => opt.MapFrom(src => src.ServiceOrder.ServiceOrderDetails.Select(x => x.Price).FirstOrDefault()))
                 .ForMember(des => des.StatusServicerOrder, opt => opt.MapFrom(src => src.ServiceOrder.Status))
                 .ForMember(des => des.ServiceOrderId, opt => opt.MapFrom(src => src.ServiceOrder.Id))
                 .ForMember(des => des.RoomBookingDetailId, opt => opt.MapFrom(src => src.ServiceOrder.RoomBookingDetailId))
                ;
        }
    }
}
