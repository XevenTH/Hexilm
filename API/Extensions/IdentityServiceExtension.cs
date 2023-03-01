using Microsoft.AspNetCore.Identity;
using Model;
using Persistence;

namespace API.Extensions;

public static class IdentityServiceExtension
{
    public static IServiceCollection UseIdentityServiceExtension(this IServiceCollection service)
    {
        service.AddIdentityCore<UserApp>(opt => {
            opt.Password.RequiredLength = 6;
            opt.Password.RequireNonAlphanumeric = true;
        })
        .AddEntityFrameworkStores<DataContext>()
        .AddSignInManager<SignInManager<UserApp>>();

        service.AddAuthentication();

        return service;
    }
}
