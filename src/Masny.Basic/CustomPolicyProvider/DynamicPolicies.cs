using System.Collections.Generic;

namespace Masny.Basic.CustomPolicyProvider
{
    public static class DynamicPolicies
    {
        public static IEnumerable<string> Get()
        {
            yield return SecurityLevel;
            yield return Rank;
        }

        public const string SecurityLevel = nameof(SecurityLevel);
        public const string Rank = nameof(Rank);
    }
}
