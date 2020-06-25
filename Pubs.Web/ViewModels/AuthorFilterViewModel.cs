using Pubs.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pubs.Web.ViewModels
{
    [Serializable]
    public class AuthorFilterViewModel
    {
        public List<Publisher> Publishers { get; set; }
        public List<Author> Authors { get; set; }
    }
}
