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
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PubsRepository.Models;

namespace PubsRepository.Context
{
    /// <summary>
    /// Entity framework database context for the Pubs database
    /// </summary>
    public class PubsContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PubsContext(DbContextOptions<PubsContext> options) : base(options)
        {
        }

        /// <summary>
        /// DbSet of <see cref="PubsRepository.Models.Author"/>
        /// </summary>
        public DbSet<Author> Authors { get; set; }

        /// <summary>
        /// DbSet of <see cref="PubsRepository.Models.Publisher"/>
        /// </summary>
        public DbSet<Publisher> Publishers { get; set; }

        /// <summary>
        /// DbSet of <see cref="PubsRepository.Models.Title"/>
        /// </summary>
        public DbSet<Title> Titles { get; set; }

        public DbSet<TitleAuthor> TitleAuthors { get; set; }

        public DbSet<TitleView> TitleViews { get; set; }

        /// <summary>
        /// Configure the mapping of database tables to entities
        /// </summary>
        /// <param name="modelBuilder"><see cref="System.Data.Entity.DbModelBuilder"/></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Author mapping
            EntityTypeBuilder<Author> author = modelBuilder.Entity<Author>();
            author.ToTable("authors");
            author.Property(a => a.AuthorID).HasColumnName("au_id");
            author.Property(a => a.FirstName).HasColumnName("au_fname");
            author.Property(a => a.LastName).HasColumnName("au_lname");
            author.Ignore(a => a.Name);
            author.Property(a => a.PhoneNumber).HasColumnName("phone");
            author.Property(a => a.Address).HasColumnName("address");
            author.Property(a => a.City).HasColumnName("city");
            author.Property(a => a.State).HasColumnName("state");
            author.Property(a => a.PostalCode).HasColumnName("zip");
            author.Property(a => a.HasContract).HasColumnName("contract");
            author.Ignore(a => a.YearToDateSales);
            author.HasKey(a => a.AuthorID);

            // Publisher mapping
            EntityTypeBuilder<Publisher> publisher = modelBuilder.Entity<Publisher>();
            publisher.ToTable("publishers");
            publisher.Property(p => p.PublisherID).HasColumnName("pub_id");
            publisher.Property(p => p.Name).HasColumnName("pub_name");
            publisher.Property(p => p.City).HasColumnName("city");
            publisher.Property(p => p.State).HasColumnName("state");
            publisher.Property(p => p.Country).HasColumnName("country");
            publisher.Ignore(p => p.YearToDateSales);
            publisher.HasKey(p => p.PublisherID);

            // Title mapping
            EntityTypeBuilder<Title> title = modelBuilder.Entity<Title>();
            title.ToTable("titles");
            title.Property(t => t.TitleID).HasColumnName("title_id");
            title.Property(t => t.BookTitle).HasColumnName("title");
            title.Property(t => t.Type).HasColumnName("type");
            title.Property(t => t.PublisherID).HasColumnName("pub_id");
            title.Property(t => t.Price).HasColumnName("price");
            title.Property(t => t.Advance).HasColumnName("advance");
            title.Property(t => t.Royalty).HasColumnName("royalty");
            title.Property(t => t.YearToDateSales).HasColumnName("ytd_sales");
            title.Property(t => t.Notes).HasColumnName("notes");
            title.Property(t => t.PublishDate).HasColumnName("pubdate");
            title.HasOne(t => t.Publisher).WithMany(p => p.Titles).HasForeignKey(t => t.PublisherID);
            title.HasKey(t => t.TitleID);

            // TitleAuthor mapping
            EntityTypeBuilder<TitleAuthor> titleAuthor = modelBuilder.Entity<TitleAuthor>();
            titleAuthor.ToTable("titleauthor");
            titleAuthor.Property(ta => ta.AuthorID).HasColumnName("au_id");
            titleAuthor.Property(ta => ta.TitleID).HasColumnName("title_id");
            titleAuthor.Property(ta => ta.AuthorOrder).HasColumnName("au_ord");
            titleAuthor.Property(ta => ta.RoyaltyPercentage).HasColumnName("royaltyper");
            titleAuthor.HasKey(ta => new { ta.AuthorID, ta.TitleID });
            titleAuthor.HasOne(ta => ta.Author).WithMany(a => a.Titles).HasForeignKey(ta => ta.AuthorID);
            titleAuthor.HasOne(ta => ta.Title).WithMany(t => t.Authors).HasForeignKey(ta => ta.TitleID);

            // TitleView mapping
            EntityTypeBuilder<TitleView> titleView = modelBuilder.Entity<TitleView>();
            titleView.ToTable("titleview");
            titleView.Property(tv => tv.BookTitle).HasColumnName("title");
            titleView.Property(tv => tv.AuthorOrder).HasColumnName("au_ord");
            titleView.Property(tv => tv.LastName).HasColumnName("au_lname");
            titleView.Property(tv => tv.Price).HasColumnName("price");
            titleView.Property(tv => tv.YearToDateSales).HasColumnName("ytd_sales");
            titleView.Property(tv => tv.PublisherID).HasColumnName("pub_id");
            titleView.HasKey(tv => new { tv.BookTitle, tv.LastName });
        }
    }
}