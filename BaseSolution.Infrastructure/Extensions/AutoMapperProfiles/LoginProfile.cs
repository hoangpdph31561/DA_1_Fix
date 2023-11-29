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
            CreateMap<UserEntity,ViewLoginInput>();
            CreateMap<LoginInputRequest, UserEntity>();
        }
    }
}
