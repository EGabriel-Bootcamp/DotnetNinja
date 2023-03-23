using System;
namespace LibraryMgt.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int PublisherId { get; set; }
        public Publisher? Publisher { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        public ICollection<Book>? Books { get; set; }
    }


    public class AuthorDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int PublisherId { get; set; }
    }

    public class AuthorPutDTO
    {
        public int AuthorId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}

