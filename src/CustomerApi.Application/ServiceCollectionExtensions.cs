using AutoMapper;
using CustomerApi.Application.Interfaces;
using CustomerApi.Application.Mapping;
using CustomerApi.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerApi.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApplicationMappingProfile));
            services.AddScoped<ICustomerService, CustomerService>();

            return services;
        }
    }
}
