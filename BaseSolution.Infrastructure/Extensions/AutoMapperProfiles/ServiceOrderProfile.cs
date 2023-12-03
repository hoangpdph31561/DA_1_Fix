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
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id))
                .ForMember(des => des.Price, opt => opt.MapFrom(src => src.Customer.ServiceOrders.SelectMany(x => x.ServiceOrderDetails).Select(x => x.Service.Price).FirstOrDefault()))
                .ForMember(des => des.Quantity, opt => opt.MapFrom(src => src.Customer.ServiceOrders.Select(x => x.ServiceOrderDetails).Count()))
                .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Customer.ServiceOrders.SelectMany(x => x.ServiceOrderDetails).Select(x => x.Service.Name).FirstOrDefault()))
                 ;

            CreateMap<ServiceOrderCreateRequest, ServiceOrderEntity>()
                  .ForPath(des => des.ServiceOrderDetails, opt => opt.MapFrom(src => new List<ServiceOrderDetailEntity>
                {
                     new ServiceOrderDetailEntity
                     {
                         ServiceId = src.ServiceId,
                     }
                }))
                .ForMember(des => des.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                ;
            ;
            CreateMap<ServiceOrderUpdateRequest, ServiceOrderEntity>();
        }
    }
}
