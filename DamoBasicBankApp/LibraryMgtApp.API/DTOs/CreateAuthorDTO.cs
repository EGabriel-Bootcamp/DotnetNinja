namespace LibraryManagementApp.DTOs
{
    public class CreateAuthorDTO
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public int PublisherId { get; set; }
    }
}
