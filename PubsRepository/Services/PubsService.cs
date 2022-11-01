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
using Microsoft.EntityFrameworkCore;
using PubsRepository.Context;
using PubsRepository.Models;
using System.Collections.Generic;
using System.Linq;

namespace PubsRepository.Services
{
    public class PubsService
    {
        private PubsContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"><see cref="Pubs.Data.Context.PubsContext"/></param>
        public PubsService(PubsContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create an author
        /// </summary>
        /// <param name="author"><see cref="Pubs.Data.Models.Author"/></param>
        public void CreateAuthor(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        /// <summary>
        /// Update an author
        /// </summary>
        /// <param name="author"><see cref="Pubs.Data.Models.Author"/></param>
        public void UpdateAuthor(Author author)
        {
            _context.Entry(author).State = EntityState.Modified;
            _context.SaveChanges();
        }

        /// <summary>
        /// Delete an author
        /// </summary>
        /// <param name="authorID">Unique author identifier</param>
        public void DeleteAuthor(string authorID)
        {
            Author author = _context.Authors.Include(a => a.Titles).FirstOrDefault(a => a.AuthorID == authorID);
            if (author != null)
            {
                _context.Authors.Remove(author);
                _context.SaveChanges();
            }
        }

        public Author GetAuthor(string authorID)
        {
            return _context.Authors.Include(a => a.Titles).ThenInclude(ta => ta.Title).FirstOrDefault(a => a.AuthorID == authorID);
        }

        public List<Author> ListAuthors()
        {
            return _context.Authors.Include(a => a.Titles).ThenInclude(ta => ta.Title).ThenInclude(t => t.Publisher).ToList();
        }

        public List<Author> ListAuthors(int startRow, int numberOfRows, string sortBy, string sortDirection, out int numberOfAuthors)
        {
            numberOfAuthors = _context.Authors.Count();
            List<Author> authors = null;

            if (sortDirection.ToLower() == "asc")
            {
                switch (sortBy)
                {
                    case "AuthorID":
                        authors = _context.Authors.Include(a => a.Titles).ThenInclude(ta => ta.Title).ThenInclude(t => t.Publisher).OrderBy(a => new { a.AuthorID }).Skip(startRow - 1).Take(numberOfRows).ToList();
                        break;
                    case "Name":
                        authors = _context.Authors.Include(a => a.Titles).ThenInclude(ta => ta.Title).ThenInclude(t => t.Publisher).OrderBy(a => new { a.LastName, a.FirstName }).Skip(startRow - 1).Take(numberOfRows).ToList();
                        break;
                    case "City":
                        authors = _context.Authors.Include(a => a.Titles).ThenInclude(ta => ta.Title).ThenInclude(t => t.Publisher).OrderBy(a => new { a.City }).Skip(startRow - 1).Take(numberOfRows).ToList();
                        break;
                    case "State":
                        authors = _context.Authors.Include(a => a.Titles).ThenInclude(ta => ta.Title).ThenInclude(t => t.Publisher).OrderBy(a => new { a.State }).Skip(startRow - 1).Take(numberOfRows).ToList();
                        break;
                    case "HasContract":
                        authors = _context.Authors.Include(a => a.Titles).ThenInclude(ta => ta.Title).ThenInclude(t => t.Publisher).OrderBy(a => new { a.HasContract }).Skip(startRow - 1).Take(numberOfRows).ToList();
                        break;
                    default:
                        authors = _context.Authors.Include(a => a.Titles).ThenInclude(ta => ta.Title).ThenInclude(t => t.Publisher).OrderBy(a => new { a.LastName, a.FirstName }).Skip(startRow - 1).Take(numberOfRows).ToList();
                        break;
                }
            }
            else
            {
                switch (sortBy)
                {
                    case "AuthorID":
                        authors = _context.Authors.Include(a => a.Titles).ThenInclude(ta => ta.Title).ThenInclude(t => t.Publisher).OrderByDescending(a => new { a.AuthorID }).Skip(startRow - 1).Take(numberOfRows).ToList();
                        break;
                    case "Name":
                        authors = _context.Authors.Include(a => a.Titles).ThenInclude(ta => ta.Title).ThenInclude(t => t.Publisher).OrderByDescending(a => new { a.LastName, a.FirstName }).Skip(startRow - 1).Take(numberOfRows).ToList();
                        break;
                    case "City":
                        authors = _context.Authors.Include(a => a.Titles).ThenInclude(ta => ta.Title).ThenInclude(t => t.Publisher).OrderByDescending(a => new { a.City }).Skip(startRow - 1).Take(numberOfRows).ToList();
                        break;
                    case "State":
                        authors = _context.Authors.Include(a => a.Titles).ThenInclude(ta => ta.Title).ThenInclude(t => t.Publisher).OrderByDescending(a => new { a.State }).Skip(startRow - 1).Take(numberOfRows).ToList();
                        break;
                    case "HasContract":
                        authors = _context.Authors.Include(a => a.Titles).ThenInclude(ta => ta.Title).ThenInclude(t => t.Publisher).OrderByDescending(a => new { a.HasContract }).Skip(startRow - 1).Take(numberOfRows).ToList();
                        break;
                    default:
                        authors = _context.Authors.Include(a => a.Titles).ThenInclude(ta => ta.Title).ThenInclude(t => t.Publisher).OrderByDescending(a => new { a.LastName, a.FirstName }).Skip(startRow - 1).Take(numberOfRows).ToList();
                        break;
                }
            }

            return authors;
        }

        public List<Author> ListAuthorsByLastName(string lastName)
        {
            List<Author> authors = (from a in _context.Authors
                                    where a.LastName.StartsWith(lastName)
                                    select a).ToList();

            return authors;
        }

        public List<Author> ListAuthorsByLastName(string lastName, int startRow, int numberOfRows, out int numberOfAuthors)
        {
            numberOfAuthors = _context.Authors.Where(a => a.LastName.StartsWith(lastName)).Count();
            List<Author> authors = (from a in _context.Authors
                                    where a.LastName.StartsWith(lastName)
                                    select a).OrderBy(a => new { a.LastName, a.FirstName }).Skip(startRow - 1).Take(numberOfRows).ToList();

            return authors;
        }

        public List<Author> ListAuthorsByID(string authorID)
        {
            List<Author> authors = (from a in _context.Authors
                                    where a.AuthorID.StartsWith(authorID)
                                    select a).ToList();

            return authors;
        }

        public List<Author> ListAuthorsByID(string authorID, int startRow, int numberOfRows, out int numberOfAuthors)
        {
            numberOfAuthors = _context.Authors.Where(a => a.AuthorID.StartsWith(authorID)).Count();
            List<Author> authors = (from a in _context.Authors
                                    where a.AuthorID.StartsWith(authorID)
                                    select a).OrderBy(a => new { a.LastName, a.FirstName }).Skip(startRow - 1).Take(numberOfRows).ToList();

            return authors;
        }

        /// <summary>
        /// Create a new publisher
        /// </summary>
        /// <param name="publisher"><see cref="Pubs.Data.Models.Publisher"/></param>
        public void CreatePublisher(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            _context.SaveChanges();
        }

        public void UpdatePublisher(Publisher publisher)
        {
            _context.Entry(publisher).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeletePublisher(string publisherID)
        {
            Publisher publisher = _context.Publishers.Find(publisherID);
            if (publisher != null)
            {
                _context.Publishers.Remove(publisher);
                _context.SaveChanges();
            }
        }

        public Publisher GetPublisher(string publisherID)
        {
            return _context.Publishers.Include(p => p.Titles).FirstOrDefault(p => p.PublisherID == publisherID);
        }

        public List<Publisher> ListPublishers()
        {
            return _context.Publishers.Include(p => p.Titles).ToList();
        }

        public List<Publisher> ListPublishers(int startRow, int numberOfRows, out int numberOfPublishers)
        {
            numberOfPublishers = _context.Publishers.Count();
            return _context.Publishers.Include(p => p.Titles).Skip(startRow - 1).Take(numberOfRows).ToList();
        }

        public void CreateTitle(Title title)
        {
            _context.Titles.Add(title);
            _context.SaveChanges();
        }

        public void UpdateTitle(Title title)
        {
            _context.Entry(title).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteTitle(string titleID)
        {
            Title title = _context.Titles.Include(t => t.Authors).Include(t => t.Publisher).FirstOrDefault(t => t.TitleID == titleID);
            if (title != null)
            {
                _context.Titles.Remove(title);
                _context.SaveChanges();
            }
        }

        public Title GetTitle(string titleID)
        {
            return _context.Titles.Include(t => t.Authors).ThenInclude(ta => ta.Author).Include(t => t.Publisher).FirstOrDefault(t => t.TitleID == titleID);
        }

        public List<Title> ListTitles()
        {
            return _context.Titles.Include(t => t.Authors).ThenInclude(a => a.Author).Include(t => t.Publisher).ToList();
        }

        public List<Title> ListTitles(int startRow, int numberOfRows, out int numberOfTitles)
        {
            numberOfTitles = _context.Titles.Count();
            return _context.Titles.Include(t => t.Authors).ThenInclude(a => a.Author).Include(t => t.Publisher).Skip(startRow - 1).Take(numberOfRows).ToList();
        }

        public List<TitleView> ListTitleViews()
        {
            return _context.TitleViews.ToList();
        }
    }
}
