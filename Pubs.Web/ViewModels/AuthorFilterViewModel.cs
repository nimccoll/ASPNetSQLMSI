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
using Pubs.Data.Models;
using System;
using System.Collections.Generic;

namespace Pubs.Web.ViewModels
{
    [Serializable]
    public class AuthorFilterViewModel
    {
        public List<Publisher> Publishers { get; set; }
        public List<Author> Authors { get; set; }
    }
}
