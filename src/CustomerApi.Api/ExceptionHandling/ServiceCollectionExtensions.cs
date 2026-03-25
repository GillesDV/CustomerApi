using CustomerApi.Api.Models;
using CustomerApi.Api.Validation;
using CustomerApi.Application.Exceptions;
using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerApi.Api.ExceptionHandling
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomExceptionMapping(this IServiceCollection services)
        {
            services.AddSingleton<ExceptionMappingOptions>(sp =>
            {
                var options = new ExceptionMappingOptions();

                options.Map<EntityNotFoundException>(StatusCodes.Status404NotFound);
                options.Map<BulkInsertExceededException>(StatusCodes.Status400BadRequest);

                return options;
            });

            services.AddScoped<IValidator<CreateCustomerRequest>, CreateCustomerRequestValidator>();

            return services;
        }

    }
}
