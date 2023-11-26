using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Statistic.RoomBooking;
using BaseSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.Extensions.AutoMapperProfiles
{
    public class RoomBookingStatisticProfile : Profile
    {
        public RoomBookingStatisticProfile()
        {
            CreateMap<RoomBookingEntity, RoomBookingStatisticDto>()
                     .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.RoomBookingDetails.Select(x => x.CheckInReality.Month).FirstOrDefault()))
                     .ForMember(dest => dest.NameRoom, opt => opt.MapFrom(src => src.RoomBookingDetails.Select(x => x.RoomDetail.Name).FirstOrDefault()))
                     .ForMember(dest => dest.BookingCount, opt => opt.MapFrom(src => src.RoomBookingDetails
                                                                     .GroupBy(x => new { x.RoomDetail.Name, x.CheckInReality.Month })
                                                                     .Select(g => g.Count()).First()));


        }
    }
}

