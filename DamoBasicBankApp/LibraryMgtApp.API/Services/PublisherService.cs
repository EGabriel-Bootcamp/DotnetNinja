using LibraryManagementApp.DTOs;
using LibraryManagementApp.Entities;
using LibraryManagementApp.Interfaces;
using LibraryManagementApp.LibManagementDbContext;
using LibraryManagementApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApp.Services
{
    public class PublisherService : IPublisher
    {
        private readonly LibraryMgtDbContext _libMgtDbContext;
        //private readonly AppConfig _appConfig;

        public PublisherService(LibraryMgtDbContext libMgtDbContext)
        {
            _libMgtDbContext = libMgtDbContext ?? throw new ArgumentNullException(nameof(libMgtDbContext)); 
        }

        public async Task<Response<CreatePublisherResponseDTO>> CreatePublisher(CreatePublisherDTO publisher)
        {
            if (publisher == null) return new ErrorResponse<CreatePublisherResponseDTO> { Message = "publisher cannot be null." };
            var existingPublisher = await _libMgtDbContext.Publishers.Where(p => p.Phone == publisher.Phone).FirstOrDefaultAsync();
            if (existingPublisher != null) return new Response<CreatePublisherResponseDTO> { Message = "publisher already exists" };
            Publisher publish = new()
            {
                Name = publisher.Name,
                Phone = publisher.Phone,
                Address = publisher.Address
            };
            await _libMgtDbContext.Publishers.AddAsync(publish);
            int save = await _libMgtDbContext.SaveChangesAsync();
            if (save < 0) return new ErrorResponse<CreatePublisherResponseDTO> { Message = "Unable to insert publisher.", };

            return new SuccessResponse<CreatePublisherResponseDTO> { Message = "Publisher Added successfully", 
                Content = new CreatePublisherResponseDTO
            {
                    Name = publisher.Name,
                    Phone = publisher.Phone,
                    Address = publisher.Address

            }, Success = true };
        }

        

        public async Task<IEnumerable<GetPublisherResponseDTO>> GetAllPublisher()
        {
            return await _libMgtDbContext.Publishers.Select(x=> new GetPublisherResponseDTO
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                Phone = x.Phone
            }).ToListAsync();

        }

        public async Task<Response<GetPublisherResponseDTO>> GetAPublisher(int id)
        {
            var publisher = await _libMgtDbContext.Publishers.FindAsync(id);
            if(publisher==null)return new ErrorResponse<GetPublisherResponseDTO> { Message = "Publisher not found", Code = "400" };
            return new SuccessResponse<GetPublisherResponseDTO> { Message = "Publisher successsfully found", Content = new GetPublisherResponseDTO
            {
                Id = publisher.Id,  
                Name = publisher.Name,
                Phone = publisher.Phone,
                Address = publisher.Address
            }, Success = true };
        }



        public async Task<IEnumerable<GetAuthorByPublisherIdDTO>> GetAllAuthorAttachedToPublisher(int id)
        {
            IEnumerable<GetAuthorByPublisherIdDTO> authors= await _libMgtDbContext.Authors.
                Where(p=>p.PublisherId== id).
                Select(x => new GetAuthorByPublisherIdDTO
                {
                    AuthorId = x.Id,
                    Name = x.Name,
                    DateOfBirth = x.DateOfBirth,
                    Nationality = x.Nationality,
                    Phone=x.Phone
                }).ToListAsync();
            //if (authors == null) return new Response<Author> { Message = "This Publisher does not exist" };
            return authors; 
        }
    }
    

       
    
}

