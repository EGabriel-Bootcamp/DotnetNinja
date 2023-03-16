using LibraryManagementApp.DTOs;
using LibraryManagementApp.Entities;
using LibraryManagementApp.Interfaces;
using LibraryManagementApp.LibManagementDbContext;
using LibraryManagementApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LibraryManagementApp.Services
{
    public class BookService : IBook
    {
        private readonly LibraryMgtDbContext _libMgtDbContext;
        //private readonly AppConfig _appConfig;

        public BookService(LibraryMgtDbContext libMgtDbContext)
        {
            _libMgtDbContext = libMgtDbContext?? throw new ArgumentNullException(nameof(libMgtDbContext));
        }
        public async Task<Response<CreateBookResponseDTO>> CreateBook(CreateBookDTO book)
        {
            if (book == null) return new ErrorResponse<CreateBookResponseDTO> { Message = "Book cannot be null" };
            if (book?.AuthorId == null) return new ErrorResponse<CreateBookResponseDTO> { Message = "Please provide a AuthorId for this Book",Code="404" };
            var bookAuthor = await _libMgtDbContext.Authors.FindAsync(book.AuthorId);
            if (bookAuthor == null) return new ErrorResponse<CreateBookResponseDTO> { Message = "Author doesn't exist, Book cannot be created" };
            var existingBook = await _libMgtDbContext.Books.Where(p => p.ISBN == book.ISBN).FirstOrDefaultAsync();
            if (existingBook != null) return new Response<CreateBookResponseDTO> { Message = "Book already exists" };

            Book iwe = new()
            {
                Title= book.Title,
                ISBN= book.ISBN,
                AuthorId= book.AuthorId,
                PubishedYear=book.PubishedYear,
                Description=book.Description,

            };
            await _libMgtDbContext.Books.AddAsync(iwe);
            int save = await _libMgtDbContext.SaveChangesAsync();
            if (save < 0) return new ErrorResponse<CreateBookResponseDTO> { Message = "Unable to insert publisher. Pls try again." };

            return new SuccessResponse<CreateBookResponseDTO> { Message = "Book Added successfully", Content = new CreateBookResponseDTO
            {
                Title = book.Title,
                ISBN = book.ISBN,
                AuthorId = book.AuthorId,
                PubishedYear = book.PubishedYear,
                Description = book.Description
            }, Success = true };
        }

        public async Task<Response<IEnumerable<GetBookDTO>>> GetAllBooks()
        {
            var BookList= await _libMgtDbContext.Books.Select(x => new GetBookDTO
            {
                Id = x.Id,
                ISBN = x.ISBN,
                Title = x.Title,
                Description = x.Description,
                PubishedYear = x.PubishedYear
            }).ToListAsync();
            if (BookList.Count == 0)
                return new SuccessResponse<IEnumerable<GetBookDTO>> { Message = "No books added yet", Content = Enumerable.Empty<GetBookDTO>() };
            return new SuccessResponse<IEnumerable<GetBookDTO>> { Message = $"{BookList.Count} books found", Content = BookList };
        }

        public async Task<Response<GetBookDTO>> GetBook(int id)
        {
            var author = await _libMgtDbContext.Books.FindAsync(id);
            if (author == null) return new ErrorResponse<GetBookDTO> { Message = "Book not found", Code="400" };
            return new SuccessResponse<GetBookDTO> { Message = "Book successsfully found", Content = new GetBookDTO
            {
                Id = id,
                ISBN= author.ISBN,
                Title = author.Title,   
                Description = author.Description,
                PubishedYear= author.PubishedYear,

            }, Success = true };
        }
    }
}
