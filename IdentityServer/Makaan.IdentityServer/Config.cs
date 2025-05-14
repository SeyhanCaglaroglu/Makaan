// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace Makaan.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("resource_catalog")
                {
                    Scopes = { "catalog.read", "catalog.create", "catalog.update","catalog.delete","catalogfullpermission","property.create"},
                    ApiSecrets = new[] {new Secret("secret1".Sha256())}
                },
                new ApiResource("resource_comment")
                {
                    Scopes = { "comment.read", "comment.create", "comment.update","comment.delete","commentfullpermission"},
                    ApiSecrets = new[] {new Secret("secret2".Sha256())}
                },
                new ApiResource("resource_notification")
                {
                    Scopes = { "notification.read", "notification.create", "notification.update", "notification.delete", "notificationfullpermission"},
                    ApiSecrets = new[] {new Secret("secret2".Sha256())}
                },
                new ApiResource("resource_message")
                {
                    Scopes = { "message.read", "message.create", "message.update", "message.delete", "messagefullpermission"},
                    ApiSecrets = new[] {new Secret("secret2".Sha256())}
                },
                new ApiResource("resource_ocelot")
                {
                    Scopes = { "ocelotfullpermission"},
                    ApiSecrets = new[] {new Secret("secret2".Sha256())}
                },

                new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>()
            {
                //catalog
                new ApiScope("catalog.read","catalog icin okuma izni"),
                new ApiScope("catalog.create","catalog icin yazma izni"),
                new ApiScope("catalog.update","catalog icin guncelleme izni"),
                new ApiScope("catalog.delete","catalog icin silme izni"),
                new ApiScope("catalogfullpermission","catalog icin tam izin"),
                new ApiScope("property.create","property icin ekleme izni"),

                //comment
                new ApiScope("commentfullpermission","comment Mikroservisine full erisim"),
                new ApiScope("comment.read","comment Mikroservisine okuma erisimi"),
                new ApiScope("comment.create","comment Mikroservisine ekleme erisimi"),
                new ApiScope("comment.update","comment Mikroservisine guncelleme erisimi"),
                new ApiScope("comment.delete","comment Mikroservisine silme erisimi"),

                //notification
                new ApiScope("notificationfullpermission","notification Mikroservisine full erisim"),
                new ApiScope("notification.read","notification Mikroservisine okuma erisimi"),
                new ApiScope("notification.create","notification Mikroservisine ekleme erisimi"),
                new ApiScope("notification.update","notification Mikroservisine guncelleme erisimi"),
                new ApiScope("notification.delete","notification Mikroservisine silme erisimi"),
                
                //message
                new ApiScope("messagefullpermission","message Mikroservisine full erisim"),
                new ApiScope("message.read","message Mikroservisine okuma erisimi"),
                new ApiScope("message.create","message Mikroservisine ekleme erisimi"),
                new ApiScope("message.update","message Mikroservisine guncelleme erisimi"),
                new ApiScope("message.delete","message Mikroservisine silme erisimi"),

                //ocelot
                new ApiScope("ocelotfullpermission","ocelot a full erisim"),

                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),

                new IdentityResource
                {
                    Name = "CountryAndCity",
                    DisplayName = "Country And City",
                    Description = "Kullanici Ulke ve Sehir bilgisi",
                    UserClaims = new[] {"country","city"}
                },

                new IdentityResource
                {
                    Name= "Roles",
                    DisplayName = "role",
                    Description = "Kullanicinin Rolleri",
                    UserClaims = new [] {"role"}
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                //Admin
                new Client()
                {
                    ClientId = "Admin_client",
                    ClientName = "MakaanAdminClient",
                    ClientSecrets = new [] {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId , IdentityServerConstants.StandardScopes.Profile, IdentityServerConstants.StandardScopes.OfflineAccess, "CountryAndCity","Roles",IdentityServerConstants.LocalApi.ScopeName, "catalogfullpermission","commentfullpermission","ocelotfullpermission","notificationfullpermission","messagefullpermission" },
                    AccessTokenLifetime = 2 * 60 * 60,
                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int) (DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds
                },
                //EstateAgent
                new Client()
                {
                    ClientId = "EstateAgent_client",
                    ClientName = "MakaanEstateAgentClient",
                    ClientSecrets = new [] {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId , IdentityServerConstants.StandardScopes.Profile, IdentityServerConstants.StandardScopes.OfflineAccess, "CountryAndCity","Roles",IdentityServerConstants.LocalApi.ScopeName, "catalog.read", "comment.read", "comment.create", "property.create", "notification.read", "ocelotfullpermission","messagefullpermission" },
                    AccessTokenLifetime = 2 * 60 * 60,
                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int) (DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds
                },
                //Member
                new Client()
                {
                    ClientId = "Member_client",
                    ClientName = "MakaanMemberClient",
                    ClientSecrets = new [] {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId , IdentityServerConstants.StandardScopes.Profile, IdentityServerConstants.StandardScopes.OfflineAccess, "CountryAndCity","Roles",IdentityServerConstants.LocalApi.ScopeName, "catalog.read", "comment.read", "comment.create", "notification.read","ocelotfullpermission","messagefullpermission" },
                    AccessTokenLifetime = 2 * 60 * 60,
                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int) (DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds
                },
                //Visitor
                new Client()
                {
                    ClientId = "Visitor_client",
                    ClientName = "MakaanVisitorClient",
                    ClientSecrets = new [] {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId , IdentityServerConstants.StandardScopes.Profile, "CountryAndCity","Roles",IdentityServerConstants.LocalApi.ScopeName,"catalog.read","comment.read","ocelotfullpermission","notification.create" },
                    AccessTokenLifetime = 2 * 60 * 60,
                    
                }
            };
        }
    }
}