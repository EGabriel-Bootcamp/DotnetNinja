using LibraryManagementApp.DTOs;
using LibraryManagementApp.Entities;
using LibraryManagementApp.Models;

namespace LibraryManagementApp.Interfaces
{
    public interface IPublisher
    {
        Task<Response<CreatePublisherResponseDTO>> CreatePublisher(CreatePublisherDTO publisher);
        Task<Response<GetPublisherResponseDTO>> GetAPublisher(int id);
        Task<IEnumerable<GetPublisherResponseDTO>> GetAllPublisher();
        Task<IEnumerable<GetAuthorByPublisherIdDTO>> GetAllAuthorAttachedToPublisher(int id);
    }
}
