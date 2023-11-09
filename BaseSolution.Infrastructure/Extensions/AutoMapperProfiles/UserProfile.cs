using AutoMapper;
using BaseSolution.Domain.Entities;
using BaseSolution.Application.DataTransferObjects.User.Request;
using BaseSolution.Application.DataTransferObjects.User;

namespace BaseSolution.Infrastructure.Extensions.AutoMapperProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity, UserDTO>();
        CreateMap<UserCreateRequest, UserEntity>();
        CreateMap<UserUpdateRequest, UserEntity>();
        CreateMap<UserDeleteRequest, UserEntity>();
    }
}
