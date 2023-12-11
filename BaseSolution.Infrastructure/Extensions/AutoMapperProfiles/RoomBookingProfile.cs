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
            CreateMap<RoomBookingEntity, RoombookingDTO>()
                     .ForMember(des => des.NameCustomer, otp => otp.MapFrom(src => src.Customer.Name))
                     .ForMember(des => des.CustomerId, otp => otp.MapFrom(src => src.Customer.Id))
                     .ForMember(des => des.StaffName, otp => otp.MapFrom(src => src.User.Name))
                     .ForMember(des => des.NameBuilding, otp => otp.MapFrom(src => src.RoomBookingDetails.Select(x => x.RoomDetail.Floor.Building.Name).FirstOrDefault()))
                     .ForMember(des => des.NameFloor, otp => otp.MapFrom(src => src.RoomBookingDetails.Select(x => x.RoomDetail.Floor.Name).FirstOrDefault()))
                     .ForMember(des => des.NameRoom, otp => otp.MapFrom(src => src.RoomBookingDetails.Select(x => x.RoomDetail.Name).FirstOrDefault()))
                     .ForMember(des => des.ServiceOrderId, otp => otp.MapFrom(src => src.RoomBookingDetails.SelectMany(x => x.ServiceOrders).Select(x => x.Id).FirstOrDefault()))

                     .ForMember(des => des.RoomDetailId, otp => otp.MapFrom(src => src.RoomBookingDetails.Select(x => x.RoomDetail.Id).FirstOrDefault()))

                     // room

                     .ForMember(des => des.RoomPrice, otp => otp.MapFrom(src => src.RoomBookingDetails.Select(x => x.RoomDetail.Price).FirstOrDefault()))
                     .ForMember(des => des.PrePaid, otp => otp.MapFrom(src => src.RoomBookingDetails.Select(x => x.PrePaid).FirstOrDefault()))
                     .ForMember(des => des.CheckInReality, otp => otp.MapFrom(src => src.RoomBookingDetails.Select(x => x.CheckInReality).FirstOrDefault()))
                     .ForMember(des => des.CheckOutReality, otp => otp.MapFrom(src => src.RoomBookingDetails.Select(x => x.CheckOutReality).FirstOrDefault()))
                      .ForMember(des => des.CheckInBooking, otp => otp.MapFrom(src => src.RoomBookingDetails.Select(x => x.CheckInBooking).FirstOrDefault()))
                     .ForMember(des => des.CheckOutBooking, otp => otp.MapFrom(src => src.RoomBookingDetails.Select(x => x.CheckOutBooking).FirstOrDefault()))
                     .ForMember(des => des.NameRoomType, otp => otp.MapFrom(src => src.RoomBookingDetails.Select(x => x.RoomDetail.RoomType.Name).FirstOrDefault()))
                     .ForMember(des => des.StatusRoom, otp => otp.MapFrom(src => src.RoomBookingDetails.Select(x => x.RoomDetail.Status).FirstOrDefault()))
                     .ForMember(des => des.RoomBookingDetailId, otp => otp.MapFrom(src => src.RoomBookingDetails.Select(x => x.Id).FirstOrDefault()))

                     // service
                     .ForMember(des => des.TotalService, otp => otp.MapFrom(src => src.RoomBookingDetails.SelectMany(x => x.ServiceOrders).SelectMany(x => x.ServiceOrderDetails).Select(x => x.ServiceId).Count())) // tổng số service hiển thị ra mockup
                     .ForMember(des => des.ServicePrice, otp => otp.MapFrom(src => src.RoomBookingDetails.SelectMany(x => x.ServiceOrders).SelectMany(x => x.ServiceOrderDetails).Select(x => x.Service.Price).FirstOrDefault())) 
                     .ForMember(des => des.NameService, otp => otp.MapFrom(src => src.RoomBookingDetails.SelectMany(x => x.ServiceOrders).SelectMany(x => x.ServiceOrderDetails).Select(x => x.Service.Name).FirstOrDefault()));
                    
            CreateMap<RoombookingCreateRequest, RoomBookingEntity>()
                .ForPath(des => des.RoomBookingDetails,opt => opt.MapFrom(src => new List<RoomBookingDetailEntity>
                {
                     new RoomBookingDetailEntity // tạo mới 1 đối tượng RoomBookingDetailEntity từ RoombookingCreateRequest truyền xuống
                     {
                         RoomDetailId = src.RoomDetailId,
                         CheckInBooking = src.CheckInBooking,
                         CheckOutBooking = src.CheckOutBooking,
                         CheckInReality = src.CheckInBooking,
                         CheckOutReality =  src.CheckOutBooking,
                         RoomBookingId = src.Id,
                         Price = src.Price

                     }
                }))
                .ForMember(des => des.CustomerId,opt => opt.MapFrom(src => src.CustomerId))
                ;
            CreateMap<RoombookingUpdateRequest, RoomBookingEntity>()
             .ForPath(des => des.RoomBookingDetails, opt => opt.MapFrom(src => new List<RoomBookingDetailEntity>
                {
                     new RoomBookingDetailEntity
                     {
                         RoomDetailId = src.RoomDetailId,
                          CheckOutReality = src.CheckOutReality
                     }
                }))
                .ForMember(des => des.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                ;
            CreateMap<RoombookingDeleteRequest, RoomBookingEntity>();
        }
    }
}
