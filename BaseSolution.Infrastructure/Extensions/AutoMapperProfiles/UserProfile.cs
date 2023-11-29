using AutoMapper;
using BaseSolution.Domain.Entities;
using BaseSolution.Application.DataTransferObjects.User.Request;
using BaseSolution.Application.DataTransferObjects.User;

namespace BaseSolution.Infrastructure.Extensions.AutoMapperProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity, UserDTO>()
            .ForMember(des => des.RoleCode, opt => opt.MapFrom(src => src.UserRole.RoleCode));
        CreateMap<UserCreateRequest, UserEntity>();
        CreateMap<UserUpdateRequest, UserEntity>();
        CreateMap<UserDeleteRequest, UserEntity>();
    }
}
