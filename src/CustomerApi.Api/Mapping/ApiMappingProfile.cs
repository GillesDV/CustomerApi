using AutoMapper;
using CustomerApi.Api.Models;
using CustomerApi.Application.DTO;

namespace CustomerApi.Api.Mapping
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<CreateCustomerRequest, CustomerDto>()
                .ForMember(destination => destination.Id, options => options.Ignore())
                .ForMember(destination => destination.Orders, options => options.Ignore());
            CreateMap<CustomerDto, CustomerResponse>();
        }
    }
}
