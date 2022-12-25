using Persistence;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Application.Movies;

namespace API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection UseServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy", policy =>
            {
                policy
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("http://localhost:5173");
            });
        });

        services.AddMediatR(typeof(List.Handler));

        return services;
    }
}
