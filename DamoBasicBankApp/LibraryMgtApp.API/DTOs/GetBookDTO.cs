namespace LibraryManagementApp.DTOs
{
    public class GetBookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int PubishedYear { get; set; }
        public string Description { get; set; }
    }

}
