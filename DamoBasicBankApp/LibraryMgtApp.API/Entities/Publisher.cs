namespace LibraryManagementApp.Entities
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public ICollection<Book> Books { get; set; }
        public ICollection<Author> Authors { get; set; }
    }
}