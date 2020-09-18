using Microsoft.AspNetCore.Authorization;

namespace Masny.Basic.CustomPolicyProvider
{
    public class SecurityLevelRequirement : IAuthorizationRequirement
    {
        public int Level { get; }

        public SecurityLevelRequirement(int level)
        {
            Level = level;
        }
    }
}
