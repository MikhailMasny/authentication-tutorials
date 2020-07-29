﻿using IdentityModel;
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
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiResource> GetApis() =>
            new List<ApiResource> 
            {
                new ApiResource("Masny.IdentityApiOne"),
                new ApiResource("Masny.IdentityApiTwo"),
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
                    RedirectUris = { "https://localhost:44317/signin-oidc" },
                    AllowedScopes = {
                        "ApiOne",
                        "ApiTwo",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                    },
                    RequireConsent = false,
                },
            };
    }
}
