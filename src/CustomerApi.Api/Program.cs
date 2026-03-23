using CustomerApi.Api.Mapping;
using CustomerApi.Application;
using CustomerApi.Infrastructure;
using Microsoft.OpenApi.Models;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddApplicationLayer();
        builder.Services.AddAutoMapper(typeof(ApiMappingProfile));
        builder.Services.AddInfrastructureLayer();

        //TODO register proper logging, error handling, automapper

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Customer API",
                Description = "Now with up to 100% more fake data!",
                Version = "v1"
            });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
