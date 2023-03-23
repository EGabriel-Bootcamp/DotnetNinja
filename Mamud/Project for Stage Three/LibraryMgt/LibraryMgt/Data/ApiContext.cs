using System;
using System.Xml;
using LibraryMgt.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryMgt.Data
{
	public class ApiContext : DbContext
	{
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }
    }
}

