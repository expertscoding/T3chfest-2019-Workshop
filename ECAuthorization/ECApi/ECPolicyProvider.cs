using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECAuthorization.ECApi
{
    public class ECPolicyProvider : IAuthorizationPolicyProvider
    {
        const string DAY_PREFIX = "DAY_";

        public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

        public ECPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            var policyBuilder = new AuthorizationPolicyBuilder();

            if (policyName.StartsWith(DAY_PREFIX))
            {
                if (int.TryParse(policyName.Substring(DAY_PREFIX.Length), out var day))
                {
                    policyBuilder.AddRequirements(new DayRequirement(day));

                    return Task.FromResult(policyBuilder.Build());
                }
                
            }

            return FallbackPolicyProvider.GetPolicyAsync(policyName);
        }
    }
}
