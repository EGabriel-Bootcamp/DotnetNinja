using LibraryManagementApp.DTOs;
using LibraryManagementApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementApp.Controllers
{
    public class BooksController:RootController
    {
        private readonly IBook _book;
        public BooksController(IBook book)
        {
            _book = book;
        }

        [HttpPost]
        [Route("CreateBook")]
        public async Task<IActionResult> CreateBook(CreateBookDTO Book)
        {
            try
            {
                var response = await _book.CreateBook(Book);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while processing the request: " + ex.Message.ToString());
            }
        }

        [HttpGet]
        [Route("GetAllBook")]
        public async Task<IActionResult> GetAllBook()
        {
            try
            {
                var response = await _book.GetAllBooks();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while processing the request: " + ex.Message.ToString());
            }
        }


        [HttpGet]
        [Route("GetABook/{id}")]
        public async Task<IActionResult> GetABook(int id)
        {
            try
            {
                var response = await _book.GetBook(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while processing the request: " + ex.Message.ToString());
            }
        }
    }
}
