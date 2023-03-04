using Persistence;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Application.Movies;
using API.Services;
using Application.Core;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SecurityInfrastructure;
using Application.Interface;

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
        services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddScoped<TokenFactory>();
        services.AddScoped<IUserAccessor, UserAccessor>();

        return services;
    }
}
