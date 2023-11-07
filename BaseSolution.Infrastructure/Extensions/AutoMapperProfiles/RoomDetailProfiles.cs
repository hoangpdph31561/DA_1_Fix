using AutoMapper;
using BaseSolution.Application.DataTransferObjects.RoomDetail;
using BaseSolution.Application.DataTransferObjects.RoomDetail.Request;
using BaseSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.Extensions.AutoMapperProfiles
{
    public class RoomDetailProfiles : Profile
    {
        public RoomDetailProfiles()
        {
            CreateMap<RoomDetailEntity,RoomDetailDto>();
            CreateMap<RoomDetailCreateRequest, RoomDetailEntity>();
            CreateMap<RoomDetailUpdateRequest, RoomDetailEntity>();
        }
    }
}
