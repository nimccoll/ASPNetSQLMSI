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
    /// <summary>
    /// Represents an author
    /// </summary>
    [Serializable]
    public class Author
    {
        #region Properties

        /// <summary>
        /// Unique author identifier
        /// </summary>
        [DisplayName("Author ID")]
        [Required]
        public string AuthorID { get; set; }
        [DisplayName("First Name")]
        [Required]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [Required]
        public string LastName { get; set; }
        public string Name
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
            set
            {

            }
        }
        [DisplayName("Phone Number")]
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [DisplayName("Zip Code")]
        [Required]
        public string PostalCode { get; set; }
        [DisplayName("Has Contract?")]
        [Required]
        public bool HasContract { get; set; }

        public virtual ICollection<TitleAuthor> Titles { get; set; }

        /// <summary>
        /// Year to date sales
        /// </summary>
        [DisplayName("YTD Sales")]
        public int YearToDateSales { get; set; }

        #endregion
    }
}