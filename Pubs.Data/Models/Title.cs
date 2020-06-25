using System;
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
    public class Title
    {
        #region Properties

        [DisplayName("Title ID")]
        [Required]
        [DataMember]
        public string TitleID { get; set; }
        [DisplayName("Title")]
        [DataMember]
        public string BookTitle { get; set; }
        [DisplayName("Category")]
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public virtual Publisher Publisher { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public decimal Advance { get; set; }
        [DataMember]
        public int Royalty { get; set; }
        [DisplayName("YTD Sales")]
        [DataMember]
        public int YearToDateSales { get; set; }
        [DataMember]
        public string Notes { get; set; }
        [DisplayName("Date Published")]
        [DataMember]
        public DateTime PublishDate { get; set; }
        [DataMember]
        public virtual ICollection<Author> Authors { get; set; }
        #endregion
    }
}