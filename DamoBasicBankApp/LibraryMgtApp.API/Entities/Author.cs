using System.Diagnostics.Eventing.Reader;

namespace LibraryManagementApp.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public int PublisherId { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
