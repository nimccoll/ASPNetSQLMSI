﻿//===============================================================================
// Microsoft FastTrack for Azure
// Managed Identity to Azure SQL Database Samples
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
using Microsoft.ApplicationInsights;
using Pubs.Data.Models;
using Pubs.Services;
using Pubs.Services.Contracts;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Pubs.Web.Controllers
{
    public class PublisherController : Controller
    {
        private TelemetryClient _telemetryClient;
        private IPubsService _pubsService;

        public PublisherController()
        {
            _telemetryClient = new TelemetryClient();
            _pubsService = new PubsService();
        }

        public PublisherController(IPubsService pubsService)
        {
            _telemetryClient = new TelemetryClient();
            _pubsService = pubsService;
        }

        // GET: Publisher
        public ActionResult Index()
        {
            _telemetryClient.TrackTrace("Publisher Index: Retrieving Publishers...");
            List<Publisher> model = _pubsService.ListPublishers();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ModelState.Clear();
            ViewBag.States = new SelectList(State.List, "Key", "Value");
            Publisher model = new Publisher();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Publisher model)
        {
            if (ModelState.IsValid)
            {
                _pubsService.CreatePublisher(model);
                TempData["Message"] = "Publisher added!";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            ModelState.Clear();
            Publisher model = _pubsService.GetPublisher(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Publisher model)
        {
            if (ModelState.IsValid)
            {
                _pubsService.UpdatePublisher(model);
            }

            return View("Index");
        }
    }
}