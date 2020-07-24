using Microsoft.AspNetCore.Authorization;

namespace Masny.Basic.AuthorizationRequirments
{
    public static class AuthorizationPolicyBuilderExtensions
    {
        public static AuthorizationPolicyBuilder RequireCustomClaim(this AuthorizationPolicyBuilder builder, string claimType)
        {
            builder.AddRequirements(new CustomRequireClaim(claimType));
            return builder;
        }
    }
}
