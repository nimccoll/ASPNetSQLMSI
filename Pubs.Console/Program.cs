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
using Pubs.Data.Models;
using Pubs.Services;
using Pubs.Services.Contracts;
using System.Collections.Generic;

namespace Pubs.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            IPubsService pubsService = new PubsService();

            List<Author> model = pubsService.ListAuthors();
            System.Console.WriteLine($"There are {model.Count} authors in the database");
        }
    }
}
