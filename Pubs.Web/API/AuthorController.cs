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
using System.Web.Http;

namespace Pubs.Web.API
{
    public class AuthorController : ApiController
    {
        private IPubsService _pubsService;

        public AuthorController()
        {
            _pubsService = new PubsService();
        }

        public AuthorController(IPubsService pubsService)
        {
            _pubsService = pubsService;
        }

        // GET api/<controller>
        public List<Author> Get()
        {
            return _pubsService.ListAuthors();
        }

        // GET api/<controller>/5
        public Author Get(string id)
        {
            return _pubsService.GetAuthor(id);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}