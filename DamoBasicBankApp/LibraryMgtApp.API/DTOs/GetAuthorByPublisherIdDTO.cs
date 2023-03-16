namespace LibraryManagementApp.DTOs
{
    public class GetAuthorByPublisherIdDTO
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
    }
}
