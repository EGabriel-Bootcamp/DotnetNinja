using LibraryManagementApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApp.LibManagementDbContext
{
    public class LibraryMgtDbContext:DbContext
    {
        public LibraryMgtDbContext(DbContextOptions<LibraryMgtDbContext> opts) : base(opts)
        {

        }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
    }
}
