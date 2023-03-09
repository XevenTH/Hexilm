using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Model;
using Persistence;

namespace API.Extensions;

public static class IdentityServiceExtension
{
    public static IServiceCollection UseIdentityServiceExtension(this IServiceCollection service, IConfiguration _config)
    {
        service.AddIdentityCore<UserApp>(opt =>
        {
            opt.Password.RequireNonAlphanumeric = false;
        })
        .AddEntityFrameworkStores<DataContext>()
        .AddSignInManager<SignInManager<UserApp>>();

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"]));
        var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false,
            };
        });

        return service;
    }
}
