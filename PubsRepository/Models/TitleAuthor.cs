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
using System;

namespace PubsRepository.Models
{
    [Serializable]
    public class TitleAuthor
    {
        public string AuthorID { get; set; }
        public virtual Author Author { get; set; }
        public string TitleID { get; set; }
        public virtual Title Title { get; set; }
        public byte? AuthorOrder { get; set; }
        public int? RoyaltyPercentage { get; set; }
    }
}
