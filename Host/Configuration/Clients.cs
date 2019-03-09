// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace Host.Configuration
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "ClientECApi",
                    ClientName = "EC Films API",
                    ClientUri = "http://www.expertscoding.es",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret("CliEnt.ExpertsCoding.SecReT".Sha256()){Type="SharedSecret"}},

                    AllowedScopes = { "ECApi", "AnotherApi" }
                },

                ///////////////////////////////////////////
                // MVC Hybrid Flow Samples
                //////////////////////////////////////////
                new Client
                {
                    ClientId = "ECApp",
                    ClientName = "EC Application",
                    ClientUri = "http://www.expertscoding.es",

                    ClientSecrets = 
                    {
                        new Secret("secret".Sha256())
                    },
                    
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    AllowAccessTokensViaBrowser = false,

                    //RedirectUris = { "http://localhost:19153/signin-oidc" },
                    //FrontChannelLogoutUri = "http://localhost:19153/signout-oidc",
                    //PostLogoutRedirectUris = { "http://localhost:19153/signout-callback-oidc" },
                    RedirectUris = { "http://localhost:21402/signin-oidc" },
                    FrontChannelLogoutUri = "http://localhost:21402/signout-oidc",
                    PostLogoutRedirectUris = { "http://localhost:21402/signout-callback-oidc" },
                    //RedirectUris = { "http://localhost:5002/signin-oidc" },
                    //FrontChannelLogoutUri = "http://localhost:5002/signout-oidc",
                    //PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AlwaysSendClientClaims = true,

                    AllowedScopes = 
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "custom.profile",
                        "ECApi"
                    }
                },
                ///////////////////////////////////////////
                // Android native app
                //////////////////////////////////////////
                new Client
                {
                    ClientId = "T3chfest-DroidNative",
                    ClientName = "Android EC Application",
                    ClientUri = "http://www.expertscoding.es",
                    
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,

                    RedirectUris = { "com.ec.T3chfest-DroidNative:/callback" },

                    AllowOfflineAccess = true,
                    AlwaysSendClientClaims = true,

                    AllowedScopes = 
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OfflineAccess
                    }
                },
                ///////////////////////////////////////////
                // Postman client credentials
                //////////////////////////////////////////
                new Client
                {
                    ClientId = "postman",
                    ClientName = "Postman App",
                    ClientUri = "https://www.getpostman.com/",

                    ClientSecrets = 
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris = {"https://www.getpostman.com/oauth2/callback"},

                    AllowOfflineAccess = true,
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    AlwaysSendClientClaims = true,
                    Claims =
                    {
                        new Claim("role", "client-role"),
                        new Claim("otro", "another")
                    },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "ECApi"
                    }
                }
            };
        }
    }
}