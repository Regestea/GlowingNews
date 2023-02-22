using GlowingNews.IdentityServer.Protos;
using IdentityServer.Shared.Client.GrpcServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IdentityServer.Shared.Client.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]

public class AuthorizeByIdentityServer : Attribute, IAsyncActionFilter
{

    private readonly string? requiredRoles;

    public AuthorizeByIdentityServer(string? requiredRoles = null)
    {
        this.requiredRoles = requiredRoles;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        
        var authorizationGrpcServices = context.HttpContext.RequestServices.GetService<AuthorizationGrpcServices>() ?? throw new ArgumentNullException(nameof(AuthorizationGrpcServices));

        if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
        {
            var jwtToken = authorizationHeader.ToString().Replace("Bearer ", "");

            var response = await authorizationGrpcServices.ValidateTokenAsync(jwtToken);

            if (response.Valid)
            {
                if (requiredRoles == null)
                {
                    return;
                }

                var roleList = response.Roles.Split(",");
                if (requiredRoles.Contains("|"))
                {
                    bool showStatus403 = true;
                    var requiredRoleList = requiredRoles.Split("|");
                    foreach (var requiredRole in requiredRoleList)
                    {
                        if (roleList.Any(x=>x==requiredRole))
                        {
                            showStatus403 = false;
                        }
                    }

                    if (showStatus403)
                    {
                        context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                    }
                }
                else
                {
                    var requiredRoleList = requiredRoles.Split(",");

                    foreach (var requiredRole in requiredRoleList)
                    {
                        if (!roleList.Any(x => x == requiredRole))
                        {
                            context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                        }
                    }
                }
                
                return;
            }

            context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            return;
        }
        else
        {
            context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
        }

    }
}