using Pubs.Data.Models;
using Pubs.Services.Contracts;
using Pubs.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pubs.Web.ViewModels;

namespace Pubs.Web.Controllers
{
    public class TitleController : Controller
    {
        private IPubsService _pubsService;

        public TitleController()
        {
            _pubsService = new PubsService();
        }

        // GET: Title
        public ActionResult Index()
        {
            List<Title> model = _pubsService.ListTitles();
            return View(model);
        }

        [HttpGet]
        public ActionResult TableEdit()
        {
            TitleEditViewModel model = new TitleEditViewModel();
            model.Titles = _pubsService.ListTitles();
            model.Authors = _pubsService.ListAuthors();
            model.PublisherSelectList = new SelectList(_pubsService.ListPublishers(), "PublisherID", "Name");
            return View(model);
        }

        [HttpPost]
        public ActionResult TableEdit(TitleEditViewModel model)
        {
            TitleEditViewModel titles = model;
            foreach (Title title in model.Titles)
            {
                title.Publisher = _pubsService.GetPublisher(title.Publisher.PublisherID);
                title.Authors = new List<Author>();
                string titleAuthorString = this.Request.Form[string.Format("ddlAuthors-{0}[]", title.TitleID)];
                if (!string.IsNullOrEmpty(titleAuthorString))
                {
                    string[] titleAuthors = titleAuthorString.Split(new char[] { ',' });
                    foreach (string authorID in titleAuthors)
                    {
                        Author author = _pubsService.GetAuthor(authorID);
                        if (title.Authors == null) title.Authors = new List<Author>();
                        title.Authors.Add(author);
                    }
                }
                _pubsService.UpdateTitle(title);
            }
            //return View(model);
            return RedirectToAction("TableEdit");
        }

        [HttpGet]
        public ActionResult Create()
        {
            List<Publisher> publishers = _pubsService.ListPublishers();
            ModelState.Clear();
            Title model = new Title();
            model.Publisher = publishers[0];
            model.Authors = new List<Author>();
            ViewBag.Authors = _pubsService.ListAuthors();
            ViewBag.Publishers = new SelectList(publishers, "PublisherID", "Name", publishers[0].PublisherID);
            return View("Create2", model);
        }

        [HttpPost]
        public ActionResult Create(Title model)
        {
            if (!string.IsNullOrEmpty(model.Publisher.PublisherID))
            {
                model.Publisher = _pubsService.GetPublisher(model.Publisher.PublisherID);
                ModelState.Clear();
                TryValidateModel(model);
            }

            // Construct authors collection
            if (model.Authors == null)
            {
                model.Authors = new List<Author>();
            }
            else
            {
                model.Authors.Clear();
            }
            List<Author> authors = _pubsService.ListAuthors();
            foreach (Author a in authors)
            {
                if (Request.Form[string.Format("author-{0}", a.AuthorID)] != null)
                {
                    model.Authors.Add(a);
                }
            }

            // Custom validation
            if (model.Advance > (((decimal)model.Royalty / 100) * 10000))
            {
                ModelState.AddModelError("Advance", "Advance is too large");
            }

            if (ModelState.IsValid)
            {
                _pubsService.CreateTitle(model);
                TempData["Message"] = "Title added!";
                return RedirectToAction("Index");
            }

            // There were model errors
            List<Publisher> publishers = _pubsService.ListPublishers();
            ViewBag.Authors = _pubsService.ListAuthors();
            ViewBag.Publishers = new SelectList(publishers, "PublisherID", "Name", model.Publisher.PublisherID);
            return View("Create2", model);
        }

        public ActionResult ListByPublisherID(List<string> publishers)
        {
            List<Title> titles = _pubsService.ListTitles();
            List<Title> model = (from t in titles
                                join p in publishers on t.Publisher.PublisherID equals p
                                select t).ToList();
            return PartialView("_TitleDropDownList", model);
        }
    }
}