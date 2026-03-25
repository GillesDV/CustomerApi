using CustomerApi.Application.Exceptions;
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
                options.Map<ValidationException>(StatusCodes.Status400BadRequest);
                options.Map<BulkInsertExceededException>(StatusCodes.Status400BadRequest);

                return options;
            });

            return services;
        }

    }
}
