using LibraryMgt.Data;
using LibraryMgt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryMgt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        ILogger<AuthorController> _logger;
        ApiContext _context;

        public AuthorController(ILogger<AuthorController> logger, ApiContext context)
        {
            _logger = logger;
            _context = context;
        }


        // GET: /<controller>/
        [HttpGet]
        [Route("GetAllAuthors")]
        public IActionResult GetAllAuthors()
        {
            var result = _context.Authors.ToList();
            return Ok(result);
        }

        // GET: id
        [HttpGet]
        [Route("GetAnAuthor/{authorId}")]
        public async Task<IActionResult> GetAnAuthor(int authorId)
        {
            var author = await _context.Authors.Include(x => x.Publisher).FirstOrDefaultAsync(a => a.AuthorId == authorId);

            if (author == null)
                return NotFound();

            return Ok(author);

        }

        // GET: id
        [HttpGet]
        [Route("GetBooksAttachedToAnAuthor/{authorId}")]
        public async Task<IActionResult> GetBooksAttachedToAnAuthor(int authorId)
        {
            var books = await _context.AuthorBooks.
                            Where(x => x.AuthorId == authorId)
                            .Select(x => new { x.Book.BookName, x.Book.ISBN }).ToListAsync();

            if (books == null)
                return NotFound();

            return Ok(books);
        }

        // Post: /<controller>/
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(AuthorDTO authorDTO)
        {
            if (authorDTO.PublisherId == 0)
            {
                return BadRequest("PublisherId invalid");
            }

            var publisher = await _context.Publishers.FindAsync(authorDTO.PublisherId);

            if (publisher == null)
                return NotFound("Publisher not found");

            var author = new Author { FirstName = authorDTO.FirstName, LastName = authorDTO.LastName, PublisherId = authorDTO.PublisherId };
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return Ok();
        }


        // Put: /<controller>/
        //[HttpPut]
        //public async Task<IActionResult> Put(AuthorPutDTO authorData)
        //{
        //    if (authorData == null || authorData.AuthorId == 0)
        //        return BadRequest();

        //    var author = await _context.Authors.FindAsync(authorData.AuthorId);
        //    if (author == null)
        //        return NotFound();
        //    author.FirstName = authorData.FirstName;
        //    author.LastName = authorData.LastName;
        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}


        // Delete: /<controller>/
        //[HttpDelete("{authorId}")]
        //public async Task<IActionResult> Delete(int authorId)
        //{
        //    if (authorId == 0)
        //        return BadRequest();

        //    var author = await _context.Authors.FindAsync(authorId);
        //    if (author == null)
        //        return NotFound();

        //    _context.Remove(author);
        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}
    }
}

