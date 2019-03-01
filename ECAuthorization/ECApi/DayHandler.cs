using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECAuthorization.ECApi
{

    public class DayHandler : AuthorizationHandler<DayRequirement>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DayRequirement requirement)
        {
            bool success = DateTime.Now.Day == requirement.Day;

            if (success) context.Succeed(requirement);

            return Task.CompletedTask;
        }

    }
}
