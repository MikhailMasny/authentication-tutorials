using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Masny.IdentityServer
{
    public static class IdentityConfiguration
    {
        public static IEnumerable<ApiResource> GetApis() =>
            new List<ApiResource> 
            {
                new ApiResource("Masny.IdentityApiOne"),
            };

        //public static IEnumerable<ApiScope> GetScopes() =>
        //    new List<ApiScope>
        //    {
        //        new ApiScope("Masny.IdentityApiOne")
        //    };

        public static IEnumerable<Client> GetClients() =>
            new List<Client> 
            {
                new Client 
                {
                    ClientId = "client_id",
                    ClientSecrets = { new Secret("client_secret".ToSha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "Masny.IdentityApiOne" }
                }
            };
    }
}
