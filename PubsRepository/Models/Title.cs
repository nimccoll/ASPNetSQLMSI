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
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PubsRepository.Models
{
    [Serializable]
    public class Title
    {
        #region Properties

        [DisplayName("Title ID")]
        [Required]
        public string TitleID { get; set; }
        [DisplayName("Title")]
        [Required]
        public string BookTitle { get; set; }
        [DisplayName("Category")]
        [Required]
        public string Type { get; set; }
        public string PublisherID { get; set; }
        public virtual Publisher Publisher { get; set; }
        public decimal? Price { get; set; }
        public decimal? Advance { get; set; }
        public int? Royalty { get; set; }
        [DisplayName("YTD Sales")]
        public int? YearToDateSales { get; set; }
        public string Notes { get; set; }
        [DisplayName("Date Published")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required]
        public DateTime PublishDate { get; set; }
        public virtual ICollection<TitleAuthor> Authors { get; set; }

        #endregion
    }
}