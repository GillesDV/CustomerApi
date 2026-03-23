using CustomerApi.Application.Interfaces;
using CustomerApi.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            services.AddSingleton(connection);

            services.AddDbContext<CustomerDb>((sp, options) =>
            {
                var sqliteConnection = sp.GetRequiredService<SqliteConnection>();

                options
                    .UseSqlite(sqliteConnection)
                    .EnableSensitiveDataLogging()
                    .LogTo(message => Debug.WriteLine(message), LogLevel.Information);
            });

            // Ensure the in-memory database is created once after DbContext is registered.
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<CustomerDb>();
                db.Database.EnsureCreated();
            }


            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }

    }
}
