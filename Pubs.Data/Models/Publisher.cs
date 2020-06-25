﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Pubs.Data.Models
{
    [Serializable]
    [DataContract(IsReference = true)]
    public class Publisher
    {
        #region Properties

        [DisplayName("Publisher ID")]
        [Required(ErrorMessage="Enter a publisher ID")]
        [MaxLength(4, ErrorMessage="Publisher ID cannot be longer than 4 characters")]
        [DataMember]
        public string PublisherID { get; set; }

        [Required(ErrorMessage="Enter a publisher name")]
        [MaxLength(40, ErrorMessage="Publisher name cannot be longer than 40 characters")]
        [DataMember]
        public string Name { get; set; }

        [Required(ErrorMessage="Enter a city")]
        [MaxLength(20, ErrorMessage = "City cannot be longer than 20 characters")]
        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string State { get; set; }

        [Required(ErrorMessage="Enter a country")]
        [MaxLength(30, ErrorMessage = "Country cannot be longer than 30 characters")]
        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public virtual ICollection<Title> Titles { get; set; }

        [DisplayName("YTD Sales")]
        [DataMember]
        public int YearToDateSales { get; set; }

        #endregion
    }
}