using System.Security.Claims;
using Application.Interface;
using Microsoft.AspNetCore.Http;

namespace SecurityInfrastructure;

public class UserAccessor : IUserAccessor
{
    private readonly IHttpContextAccessor _contextAccessor;

    public UserAccessor(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public string GetUsername()
    {
        return _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
    }
}
