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

        services.AddMediatR(typeof(List.Handler));

        return services;
    }
}
