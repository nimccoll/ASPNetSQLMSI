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
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PubsRepository.Context;
using PubsRepository.Models;
using PubsRepository.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace PubsCore.Web.Controllers
{
    public class AuthorController : Controller
    {
        private PubsService _pubsService;
        private readonly string[] _azureSqlScopes = new[]
        {
            "https://database.windows.net//.default"
        };

        public AuthorController(IConfiguration configuration)
        {
            // Retrieve an access token for the application's Managed Identity and set it on the SQL connection
            DefaultAzureCredential credential = new DefaultAzureCredential();
            AccessToken accessToken = credential.GetToken(new Azure.Core.TokenRequestContext(_azureSqlScopes));
            string token = accessToken.Token;
            //string token = GetAccessToken();

            if (!string.IsNullOrEmpty(token))
            {
                // Create a new SQL Server connection
                SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("PubsConnection"));
                sqlConnection.AccessToken = token;
                DbContextOptionsBuilder<PubsContext> builder = new DbContextOptionsBuilder<PubsContext>();
                builder.UseSqlServer(sqlConnection);
                DbContextOptions<PubsContext> options = builder.Options;
                PubsContext pubsContext = new PubsContext(options);
                _pubsService = new PubsService(pubsContext);
            }
        }

        //public AuthorController(IConfiguration configuration, PubsService pubsService)
        //{
        //    _pubsService = pubsService;
        //}

        public ActionResult Index()
        {
            Trace.TraceInformation("Author Index: Retrieving Authors...");
            List<Author> model = _pubsService.ListAuthors();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ModelState.Clear();
            Author model = new Author();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Author model)
        {
            if (ModelState.IsValid)
            {
                _pubsService.CreateAuthor(model);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult List(int startRow = 1, int numberOfRows = 5)
        {
            int numberOfAuthors = 0;
            List<Author> model = _pubsService.ListAuthors(startRow, numberOfRows, "Name", "asc", out numberOfAuthors);
            if (startRow > 1)
            {
                ViewBag.PreviousClass = "previous";
                ViewBag.PreviousRow = startRow - numberOfRows;
            }
            else
            {
                ViewBag.PreviousClass = "previous disabled";
                ViewBag.PreviousRow = startRow;
            }
            if (startRow + numberOfRows > numberOfAuthors)
            {
                ViewBag.NextClass = "next disabled";
                ViewBag.NextRow = numberOfAuthors;
            }
            else
            {
                ViewBag.NextClass = "next";
                ViewBag.NextRow = startRow + numberOfRows;
            }
            ViewBag.TotalRows = numberOfAuthors;
            return View("Index", model);
        }

        private string GetAccessToken()
        {
            //
            // Get an access token for SQL.
            //
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://169.254.169.254/metadata/identity/oauth2/token?api-version=2018-02-01&resource=https://database.windows.net/");
            request.Headers["Metadata"] = "true";
            request.Method = "GET";
            string accessToken = null;

            try
            {
                // Call managed identities for Azure resources endpoint.
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // Pipe response Stream to a StreamReader and extract access token.
                StreamReader streamResponse = new StreamReader(response.GetResponseStream());
                string stringResponse = streamResponse.ReadToEnd();
                JavaScriptSerializer j = new JavaScriptSerializer();
                Dictionary<string, string> list = (Dictionary<string, string>)j.Deserialize(stringResponse, typeof(Dictionary<string, string>));
                accessToken = list["access_token"];
            }
            catch (Exception e)
            {
                string errorText = String.Format("{0} \n\n{1}", e.Message, e.InnerException != null ? e.InnerException.Message : "Acquire token failed");
            }

            return accessToken;
        }
    }
}
