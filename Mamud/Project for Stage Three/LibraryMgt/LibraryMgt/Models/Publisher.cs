using System;
using System.Collections.ObjectModel;

namespace LibraryMgt.Models
{
	public class Publisher
	{
        public int PublisherId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        public ICollection<Author>? Authors { get; set; }
    }

    public class PublisherDTO
    {
        public int PublisherId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}

