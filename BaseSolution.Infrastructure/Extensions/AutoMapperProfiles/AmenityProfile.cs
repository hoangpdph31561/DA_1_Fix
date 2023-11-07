using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Amenity.Request;
using BaseSolution.Application.DataTransferObjects.Amenity;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Infrastructure.Extensions.AutoMapperProfiles
{
    public class AmenityProfile :Profile
    {
        public AmenityProfile()
        {
            CreateMap<AmenityEntity, AmenityDTO>();
            CreateMap<AmenityCreateRequest, AmenityEntity>();
            CreateMap<AmenityUpdateRequest, AmenityEntity>();
            CreateMap<AmenityCreateRequest, AmenityDTO>();
        }
    }
}
