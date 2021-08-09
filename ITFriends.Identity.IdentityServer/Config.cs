// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace ITFriends.Identity.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                // new IdentityResources.Email(),
                // new IdentityResources.Profile(),
            };
        
        public static IEnumerable<ApiScope> ApiScopes =>
            new[]
            {
                new ApiScope("itfriends", "Resource for access to all opened microservices"),
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new[]
            {
                new ApiResource("identity", "Identity microservice")
                {
                    Scopes = new List<string> { "itfriends" }
                },
                new ApiResource("topic", "Topic microservice")
                {
                    Scopes = new List<string> { "itfriends" }
                },
                new ApiResource("notifier", "Notify microservice")
                {
                    Scopes = new List<string> { "itfriends" }
                },
                new ApiResource("telegram", "Telegram bot microservice") 
                {
                    Scopes = new List<string> { "itfriends" }
                },
                new ApiResource("job", "Job manager microservice") 
                {
                    Scopes = new List<string> { "itfriends" }
                }
            };

        public static IEnumerable<Client> Clients =>
            new[] 
            {
                new Client
                {
                    ClientId = "react-client",
                    ClientName = "React client",
                    ClientUri = "http://localhost:3000",
                    RequireClientSecret = false,
                    AllowedScopes =
                    {
                        "itfriends",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },
                    
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowOfflineAccess = true
                }
            };
    }
}