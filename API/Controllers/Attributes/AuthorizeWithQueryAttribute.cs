using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Controllers.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class AuthorizeWithQueryAttribute : Attribute, IAuthorizationFilter
{
    public string Role { get; set; }
    public string[] RestrictedQueryParams { get; set; }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var RequestQueryParamsValue = context.HttpContext.Request.Query["to"];

        var isAdmin = context.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value == Role;
        var hasRestrictedQueryParams = RestrictedQueryParams.Any(q => q == RequestQueryParamsValue);

        if (!hasRestrictedQueryParams) return;
        if (isAdmin) return;

        context.Result = new JsonResult(new { message = $"Query To {RequestQueryParamsValue} Only Provieded To Admin" })
        { StatusCode = StatusCodes.Status403Forbidden };
    }
}
