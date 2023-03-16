namespace LibraryManagementApp.DTOs
{
    public class CreateBookResponseDTO
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int PubishedYear { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
    }
}
