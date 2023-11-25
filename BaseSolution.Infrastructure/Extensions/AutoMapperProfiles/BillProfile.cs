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
                // lấy ra tổng số lượng dịch vụ của khách hàng đó
                .ForMember(des => des.TotalService, opt => opt.MapFrom(src => src.Customer.ServiceOrders.Select(x => x.ServiceOrderDetails).Count()))
                // lấy ra giá của từng dịch vụ mà khách hàng đó sử dụng 
                .ForMember(des => des.ServicePrice, opt => opt.MapFrom(src => src.Customer.ServiceOrders.SelectMany(x => x.ServiceOrderDetails).Select(x => x.Service.Price).FirstOrDefault()))
                // lấy ra giá phòng của khách hàng đó 
                .ForMember(des => des.RoomPrice, opt => opt.MapFrom(src => src.Customer.RoomBookings.SelectMany(x => x.RoomBookingDetails).Select(x => x.RoomDetail.Price).FirstOrDefault()))
                ;
            CreateMap<BillCreateRequest, BillEntity>();
            CreateMap<BillUpdateRequest, BillEntity>();
        }
    }
}
