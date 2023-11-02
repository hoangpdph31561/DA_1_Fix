using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Example;
using BaseSolution.Application.DataTransferObjects.Example.Request;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Infrastructure.Extensions.AutoMapperProfiles
{
    public class ExampleProfile : Profile
    {
        public ExampleProfile()
        {
            CreateMap<ExampleEntity, ExampleDto>();
            CreateMap<ExampleCreateRequest, ExampleDto>();
        }
    }
}
