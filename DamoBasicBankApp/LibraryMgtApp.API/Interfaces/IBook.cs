using LibraryManagementApp.DTOs;
using LibraryManagementApp.Entities;
using LibraryManagementApp.Models;

namespace LibraryManagementApp.Interfaces
{
    public interface IBook
    {
        Task<Response<CreateBookResponseDTO>> CreateBook(CreateBookDTO book);
        Task<Response<GetBookDTO>> GetBook(int Id);
        Task<Response<IEnumerable<GetBookDTO>>> GetAllBooks();
    }
}
