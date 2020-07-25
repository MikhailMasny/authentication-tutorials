using Microsoft.AspNetCore.Authentication;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Masny.Basic.TransformationAuth
{
    public class ClaimsTransformation : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var hasTypeClaim = principal.Claims.Any(x => x.Type == "Type");
            if (!hasTypeClaim)
            {
                ((ClaimsIdentity)principal.Identity).AddClaim(new Claim("Type", "AnotherValue"));
            }

            return Task.FromResult(principal);
        }
    }
}
