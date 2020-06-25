using Pubs.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pubs.Web.ViewModels
{
    [Serializable]
    public class TitleEditViewModel
    {
        public List<Title> Titles { get; set; }
        public List<Author> Authors { get; set; }
        public SelectList PublisherSelectList { get; set; }
    }
}