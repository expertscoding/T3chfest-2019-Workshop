using ECApp.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECAuthorization.ECApp
{
    public class FilmAuthorizationHandler : AuthorizationHandler<VIPRequirement, Film>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, VIPRequirement requirement, Film resource)
        {
            var user = context.User;

            if (resource.Id >= 0)
            {
                context.Succeed(requirement);
            }
            else
            {
                if (user.IsInRole("VIP")) context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class VIPRequirement : IAuthorizationRequirement { }
}
