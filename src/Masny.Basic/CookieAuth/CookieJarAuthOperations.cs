using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Masny.Basic.CookieAuth
{
    public static class CookieJarAuthOperations
    {
        public static OperationAuthorizationRequirement Open = new OperationAuthorizationRequirement
        {
            Name = CookieJarOperations.Open
        };
    }
}
