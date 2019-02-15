using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECAuthorization.ECApp
{
    public class EmailHandler : AuthorizationHandler<EmailRequirement>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmailRequirement requirement)
        {
            bool success = false;

            
            var user = context.User;

            if (user.HasClaim(c => c.Type == JwtClaimTypes.Email)) success = true;

            if (requirement.VerifiedEmail)
            {
                success = success && 
                    bool.TryParse(user.FindFirst(c => c.Type == JwtClaimTypes.EmailVerified)?.Value, out var result) ? result : false;
            }

            if (success) context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
