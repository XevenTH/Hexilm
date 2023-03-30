using Microsoft.AspNetCore.Mvc.Infrastructure;

public class UserAuthMiddleware : IMiddleware
{
    private readonly IEnumerable<EndpointInfo> _adminEndpoints;
    private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

    public UserAuthMiddleware(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
    {
        _adminEndpoints = actionDescriptorCollectionProvider
            .GetEndpointsWithAdminAuthorizeAttribute();

        _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        string path = context.Request.Path;
        string method = context.Request.Method;

        var clientRequest = _adminEndpoints.FirstOrDefault(x => x.Path == path && x.HttpMethod == method);

        if (!context.User.IsInRole("admin") && clientRequest != null)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Forbidden: User does not have the necessary permissions");
            return;
        }

        await next(context);
    }
}
