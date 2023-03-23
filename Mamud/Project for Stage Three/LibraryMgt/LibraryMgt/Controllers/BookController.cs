using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryMgt.Data;
using LibraryMgt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryMgt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        ILogger<BookController> _logger;
        ApiContext _context;

        public BookController(ILogger<BookController> logger, ApiContext context)
        {
            _logger = logger;
            _context = context;
        }


        // GET: /<controller>/
        [HttpGet]
        [Route("GetAllBooks")]
        public IActionResult GetAllBooks()
        {
            var result = _context.Books.ToList();
            return Ok(result);
        }

        // GET: id
        [HttpGet]
        [Route("GetABook/{bookId}")]
        public async Task<IActionResult> GetABook(int bookId)
        {
            var book = await _context.Books.Include(x => x.Authors).FirstOrDefaultAsync(a => a.BookId == bookId);

            if (book == null)
                return NotFound();

            return Ok(book);

        }

        // Post: /<controller>/
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(BookDTO bookDTO)
        {
            var authors = bookDTO.authors;
            foreach (var author in authors)
            {
                if (author == 0)
                {
                    return BadRequest("PublisherId invalid");
                }

                var authorData = await _context.Authors.FindAsync(author);

                if (authorData == null)
                    return NotFound("Author not found");
            }

            var book = new Book { BookName = bookDTO.BookName, ISBN = bookDTO.ISBN };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            foreach (var authorId in authors)
            {
                AuthorBook AB = new();
                AB.AuthorId = authorId;
                AB.BookId = book.BookId;
                await _context.AuthorBooks.AddAsync(AB);
                await _context.SaveChangesAsync();
                //try
                //{
                //    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateException ex)
                //{
                //    Console.WriteLine(ex.Message);
                    
                //}
            }

            return Ok();
        }


        // Put: /<controller>/
        //[HttpPut]
        //public async Task<IActionResult> Put(Book bookData)
        //{
        //    if (bookData == null || bookData.BookId == 0)
        //        return BadRequest();

        //    var book = await _context.Books.FindAsync(bookData.BookId);
        //    if (book == null)
        //        return NotFound();
        //    book.BookName = bookData.BookName;
        //    book.ISBN = bookData.ISBN;
        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}


        // Delete: /<controller>/
        //[HttpDelete("{bookId}")]
        //public async Task<IActionResult> Delete(int bookId)
        //{
        //    if (bookId == 0)
        //        return BadRequest();

        //    var book = await _context.Books.FindAsync(bookId);
        //    if (book == null)
        //        return NotFound();

        //    _context.Remove(book);
        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}
    }
}

