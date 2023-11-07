using AutoMapper;
using BaseSolution.Domain.Entities;
using BaseSolution.Application.DataTransferObjects.Role;
using BaseSolution.Application.DataTransferObjects.Role.Request;

namespace BaseSolution.Infrastructure.Extensions.AutoMapperProfiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<UserRoleEntity, RoleDTO>();
            CreateMap<RoleCreateRequest, UserRoleEntity>();
            CreateMap<RoleUpdateRequest, UserRoleEntity>();
            CreateMap<RoleDeleteRequest, UserRoleEntity>();
        }
    }
}
