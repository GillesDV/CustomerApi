using CustomerApi.Application.Interfaces;
using CustomerApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Infrastructure
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddInfrastructureLayer(
       this IServiceCollection services)
        {
            services.AddDbContext<CustomerDb>(options =>
                options.UseInMemoryDatabase("dbName"));

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }

    }
}
