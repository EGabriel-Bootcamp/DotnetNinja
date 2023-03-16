using LibraryManagementApp.DTOs;
using LibraryManagementApp.Entities;
using LibraryManagementApp.Interfaces;
using LibraryManagementApp.LibManagementDbContext;
using LibraryManagementApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApp.Services
{
    public class AuthorService : IAuthor
    {
        private readonly LibraryMgtDbContext _libMgtDbContext;
        //private readonly AppConfig _appConfig;

        public AuthorService(LibraryMgtDbContext libMgtDbContext)
        {
            _libMgtDbContext = libMgtDbContext ?? throw new ArgumentNullException(nameof(libMgtDbContext));
        }
        public async Task<Response<CreateAuthorDTO>> CreateAuthor(CreateAuthorDTO Author)
        {
            if(Author == null) return new ErrorResponse<CreateAuthorDTO> { Message = "Author cannot be null", Code="400" };
            if (Author?.PublisherId == null) return new ErrorResponse<CreateAuthorDTO> { Message = "Please provide a PublisherId for this Author", Code = "400" };
            var publisher = await _libMgtDbContext.Publishers.FindAsync(Author.PublisherId);
            if (publisher == null) return new ErrorResponse<CreateAuthorDTO> { Message = "Publisher doesn't exist, Author cannot be created",Code="404" };
            var existingAuthor = await _libMgtDbContext.Authors.Where(p => p.Phone == Author.Phone).FirstOrDefaultAsync();
            if (existingAuthor != null) return new Response<CreateAuthorDTO> { Message = "Author already exists" };
            Author author = new()
            {
                Name = Author.Name,
                Nationality=Author.Nationality,
                Phone = Author.Phone,
                DateOfBirth = Author.DateOfBirth,
                PublisherId = Author.PublisherId,
            };
            await _libMgtDbContext.Authors.AddAsync(author);
            int save = await _libMgtDbContext.SaveChangesAsync();
            if (save < 0) return new ErrorResponse<CreateAuthorDTO> { Message = "Unable to insert publisher. Pls try again." };
           
            return new SuccessResponse<CreateAuthorDTO> { Message = "Publisher Added successfully", Content = new CreateAuthorDTO
            {
                Name = Author.Name,
                Nationality = Author.Nationality,
                Phone = Author.Phone,
                DateOfBirth = Author.DateOfBirth,
                PublisherId = Author.PublisherId

            }, Success = true };


        }


        public async Task<IEnumerable<GetAuthorDTO>> GetAllAuthors()
        {
            return await _libMgtDbContext.Authors.Select(x=>new GetAuthorDTO
            {
                Id = x.Id,
                Name = x.Name,
                Nationality = x.Nationality,
                Phone = x.Phone,
                DateOfBirth = x.DateOfBirth
            }).ToListAsync(); 
        }

        public async Task<Response<GetAuthorDTO>> GetAuthor(int id)
        {
            var author = await _libMgtDbContext.Authors.FindAsync(id);
            if (author == null) return new ErrorResponse<GetAuthorDTO> { Message = "Author not found", Code = "400" };
            return new SuccessResponse<GetAuthorDTO> { Message = "Author successsfully found", Content = new GetAuthorDTO
            {
                Id=author.Id,
                Name = author.Name,
                Nationality = author.Nationality,
                Phone = author.Phone,
                DateOfBirth = author.DateOfBirth
            }, Success = true };
        }

        public async Task<IEnumerable<GetBookDTO>> GetAllBooksAttachedToAuthor(int authorid)
        {
            IEnumerable<GetBookDTO> books = await _libMgtDbContext.Books.
                Where(p => p.AuthorId == authorid).
                Select(x => new GetBookDTO
                {
                    Id = x.Id,
                    Title = x.Title,
                    ISBN = x.ISBN,
                    Description = x.Description,
                    PubishedYear=x.PubishedYear
                }).ToListAsync();
            return books;
        }
    }
}
