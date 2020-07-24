using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace Masny.Basic.AuthorizationRequirments
{
    public class CustomRequireClaimHandler : AuthorizationHandler<CustomRequireClaim>
    {
        public CustomRequireClaimHandler()
        {
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       CustomRequireClaim requirement)
        {
            var hasClaim = context.User.Claims.Any(x => x.Type == requirement.ClaimType);
            if (hasClaim)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
