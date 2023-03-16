using LibraryManagementApp.DTOs;
using LibraryManagementApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementApp.Controllers
{
    public class AuthorsController : RootController
    {
        private readonly IAuthor _author;
        public AuthorsController(IAuthor author)
        {
            _author = author;
        }

        [HttpPost]
        [Route("CreateAuthor")]
        public async Task<IActionResult> CreateAuthor(CreateAuthorDTO Author)
        {
            try
            {
                var response = await _author.CreateAuthor(Author);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while processing the request: " + ex.Message.ToString());
            }
        }

        [HttpGet]
        [Route("GetAllAuthor")]
        public async Task<IActionResult> GetAllAuthor()
        {
            try
            {
                var response = await _author.GetAllAuthors();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while processing the request: " + ex.Message.ToString());
            }
        }


        [HttpGet]
        [Route("GetAuthor/{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            try
            {
                var response = await _author.GetAuthor(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while processing the request: " + ex.Message.ToString());
            }
        }


        [HttpGet]
        [Route("GetAllBooksAttachedToAnAuthor/{asuthorId}")]
        public async Task<IActionResult> GetAllBooksAttachedToAuthor(int authorid)
        {
            try
            {
                var response = await _author.GetAllBooksAttachedToAuthor(authorid);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while processing the request: " + ex.Message.ToString());
            }
        }
    }
}
