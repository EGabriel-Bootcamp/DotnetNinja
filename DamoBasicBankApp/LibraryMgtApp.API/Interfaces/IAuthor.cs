using LibraryManagementApp.DTOs;
using LibraryManagementApp.Entities;
using LibraryManagementApp.Models;

namespace LibraryManagementApp.Interfaces
{
    public interface IAuthor
    {
        Task<Response<CreateAuthorDTO>> CreateAuthor(CreateAuthorDTO Author);
        Task<Response<GetAuthorDTO>> GetAuthor(int id);
        Task<IEnumerable<GetAuthorDTO>> GetAllAuthors();
        Task<IEnumerable<GetBookDTO>> GetAllBooksAttachedToAuthor(int id);
    }
}
