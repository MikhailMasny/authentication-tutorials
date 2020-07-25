using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;

namespace Masny.Basic.CustomPolicyProvider
{
    public static class DynamicAuthorizationPolicyFactory
    {
        public static AuthorizationPolicy Create(string policyName)
        {
            var parts = policyName.Split('.');
            var type = parts.First();
            var value = parts.Last();

            switch (type)
            {
                case DynamicPolicies.Rank:
                    {
                        return new AuthorizationPolicyBuilder()
                            .RequireClaim("Rank", value)
                            .Build();
                    }

                case DynamicPolicies.SecurityLevel:
                    {
                        return new AuthorizationPolicyBuilder()
                            .AddRequirements(new SecurityLevelRequirement(Convert.ToInt32(value)))
                            .Build();
                    }

                default:
                    {
                        return null;
                    }
            }
        }
    }
}
