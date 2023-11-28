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
                     .ForMember(dest => dest.NameRoom, opt => opt.MapFrom(src => src.RoomBookingDetails.Select(x => x.RoomDetail.Name).FirstOrDefault()));
                    


        }
    }
}

