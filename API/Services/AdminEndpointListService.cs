using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;

public class EndpointInfo
{
    public string ControllerName { get; set; }
    public string ActionName { get; set; }
    public string HttpMethod { get; set; }
    public string Path { get; set; }
}

public static class AdminEndpointListService
{
    public static List<EndpointInfo> GetEndpointsWithAdminAuthorizeAttribute(this IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
    {
        var endpointInfos = new List<EndpointInfo>();

        foreach (var actionDescriptor in actionDescriptorCollectionProvider.ActionDescriptors.Items)
        {
            if (actionDescriptor.AttributeRouteInfo != null)
            {
                var httpMethods = (actionDescriptor.ActionConstraints ?? Enumerable.Empty<IActionConstraintMetadata>())
                    .OfType<HttpMethodActionConstraint>()
                    .SelectMany(x => x.HttpMethods)
                    .Distinct();

                var endpointInfo = new EndpointInfo
                {
                    ControllerName = (actionDescriptor as ControllerActionDescriptor)?.ControllerName,
                    ActionName = actionDescriptor.RouteValues["action"],
                    HttpMethod = string.Join(", ", httpMethods),
                    Path = "/" + actionDescriptor.AttributeRouteInfo.Template
                };

                if (actionDescriptor.EndpointMetadata.OfType<AuthorizeAttribute>().Select(x => x.Roles == "admin").FirstOrDefault())
                {
                    endpointInfos.Add(endpointInfo);
                }
            }
        }

        return endpointInfos;
    }
}
