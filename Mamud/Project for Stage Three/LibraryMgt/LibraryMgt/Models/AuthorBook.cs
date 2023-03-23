using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LibraryMgt.Models
{
    //[Keyless]
	public class AuthorBook
	{
        [Key]
        public int AuthorBookId { get; set; }
        //[Key]
        [Required]
		public int AuthorId { get; set; }
        public virtual Author? Author { get; set; }
        //[Key]
        [Required]
        public int BookId { get; set; }
        public virtual Book? Book { get; set; }
    }
}

//dotnet ef migrations add MyFirstMigration
// dotnet ef database update
//  dotnet ef migrations remove
//dotnet ef database update MyFirstMigration.
//dotnet ef migrations script