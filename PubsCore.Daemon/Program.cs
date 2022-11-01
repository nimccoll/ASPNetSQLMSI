//===============================================================================
// Microsoft FastTrack for Azure
// Managed Identity to Azure SQL Database Samples
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
using Azure.Core;
using Azure.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PubsRepository.Context;
using PubsRepository.Models;
using PubsRepository.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace PubsCore.Daemon
{
    class Program
    {
        static void Main(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ENVIRONMENT");

            // Setup console application to read settings from appsettings.json
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environmentName}.json", true, true);

            IConfigurationRoot configuration = configurationBuilder.Build();

            string[] _azureSqlScopes = new[]
            {
                "https://database.windows.net//.default"
            };
            DefaultAzureCredential credential = new DefaultAzureCredential();
            AccessToken accessToken = credential.GetToken(new Azure.Core.TokenRequestContext(_azureSqlScopes));
            string token = accessToken.Token;

            if (!string.IsNullOrEmpty(token))
            {
                // Create a new SQL Server connection
                SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("SQLConn"));
                sqlConnection.AccessToken = token;
                DbContextOptionsBuilder<PubsContext> builder = new DbContextOptionsBuilder<PubsContext>();
                builder.UseSqlServer(sqlConnection);
                DbContextOptions<PubsContext> options = builder.Options;
                PubsContext pubsContext = new PubsContext(options);
                PubsService pubsService = new PubsService(pubsContext);

                List<Author> model = pubsService.ListAuthors();
                System.Console.WriteLine($"There are {model.Count} authors in the database");
            }
        }
    }
}
