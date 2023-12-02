using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Account;
using BaseSolution.Application.DataTransferObjects.Account.request;
using BaseSolution.Domain.Entities;


namespace BaseSolution.Infrastructure.Extensions.AutoMapperProfiles
{
    public class LoginProfile : Profile
    {
        public LoginProfile()
        {
            CreateMap<UserEntity, ViewLoginInput>()
                .ForMember(des => des.RoleCode, opt => opt.MapFrom(src => src.UserRole.RoleCode));
                ;
            CreateMap<LoginInputRequest, UserEntity>();
        }
    }
}
