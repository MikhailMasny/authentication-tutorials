using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Masny.IdentityServer
{
    public static class IdentityConfiguration
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                //new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "rc.scope",
                    UserClaims =
                    {
                        "rc.garndma"
                    }
                }
            };

        public static IEnumerable<ApiResource> GetApis() =>
            new List<ApiResource>
            {
                new ApiResource("Masny.IdentityApiOne"),
                new ApiResource("Masny.IdentityApiTwo", new string[] { "rc.api.garndma" }),
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
                },
                new Client
                {
                    ClientId = "client_id_mvc",
                    ClientSecrets = { new Secret("client_secret_mvc".ToSha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    // RequirePkce = true, <- Uncomment this code for PKCE
                    RedirectUris = { "https://localhost:44317/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:44317/Home/Index" },
                    AllowedScopes = {
                        "Masny.IdentityApiOne",
                        "Masny.IdentityApiTwo",
                        IdentityServerConstants.StandardScopes.OpenId,
                        //IdentityServerConstants.StandardScopes.Profile,
                        "rc.scope",
                    },
                    // Puts all the claims in the id token
                    //AlwaysIncludeUserClaimsInIdToken = true,
                    AllowOfflineAccess = true,
                    RequireConsent = false,
                },
                new Client
                {
                    ClientId = "client_id_js",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    // Uncomment this code for PKCE
                    //AllowedGrantTypes = GrantTypes.Code,
                    //RequirePkce = true,
                    //RequireClientSecret = false,
                    RedirectUris = { "https://localhost:44379/home/signin" },
                    PostLogoutRedirectUris = { "https://localhost:44379/Home/Index" },
                    AllowedCorsOrigins = { "https://localhost:44379" },
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        "Masny.IdentityApiOne",
                        "Masny.IdentityApiTwo",
                        "rc.scope",
                    },
                    AccessTokenLifetime = 1,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                }
            };
    }
}
