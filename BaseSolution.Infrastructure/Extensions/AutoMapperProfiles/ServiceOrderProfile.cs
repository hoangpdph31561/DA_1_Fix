using AutoMapper;
using BaseSolution.Application.DataTransferObjects.ServiceOrder;
using BaseSolution.Application.DataTransferObjects.ServiceOrder.Request;
using BaseSolution.Domain.Entities;


namespace BaseSolution.Infrastructure.Extensions.AutoMapperProfiles
{
    public class ServiceOrderProfile : Profile
    {
        public ServiceOrderProfile()
        {
            CreateMap<ServiceOrderEntity, ServiceOrderDTO>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
                .ForMember(des => des.Price, opt => opt.MapFrom(src => src.ServiceOrderDetails.Select(x => x.Price).First()))
                .ForMember(des => des.Quantity, opt => opt.MapFrom(src => src.ServiceOrderDetails.Count()))
                .ForMember(des => des.CustomerName, opt => opt.MapFrom(src => src.ServiceOrderDetails.Count()))
                .ForMember(des => des.UserName, opt => opt.MapFrom(src => src.RoomBookingDetail.RoomBooking.User.Name))
                .ForMember(des => des.Name, opt => opt.MapFrom(src => src.ServiceOrderDetails.Select(x => x.Service.Name).First()));
                 ;

            CreateMap<ServiceOrderCreateRequest, ServiceOrderEntity>();
            CreateMap<ServiceOrderUpdateRequest, ServiceOrderEntity>();
        }
    }
}
