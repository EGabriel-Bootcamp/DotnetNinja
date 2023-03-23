using System;
namespace LibraryMgt.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public required string BookName { get; set; }
        public required string ISBN { get; set; }
        
        public ICollection<Author>? Authors { get; set; }
    }

    public class BookDTO
    {
        public required string BookName { get; set; }
        public required string ISBN { get; set; }
        public required List<int> authors { get; set; }
    }
}

