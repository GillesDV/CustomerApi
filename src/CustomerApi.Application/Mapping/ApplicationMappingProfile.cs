using AutoMapper;
using CustomerApi.Application.DTO;
using CustomerApi.Domain.Entities;

namespace CustomerApi.Application.Mapping
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
        }
    }
}
