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
    public class PublisherController : ControllerBase
    {
        ILogger<PublisherController> _logger;
        ApiContext _context;

        public PublisherController(ILogger<PublisherController> logger, ApiContext context)
        {
            _logger = logger;
            _context = context;
        }


        // GET: /<controller>/
        [HttpGet]
        [Route("GetAllPublishers")]
        public IActionResult GetAllPublishers()
        {
            var result = _context.Publishers.Include(x => x.Authors).ToList();
            return Ok(result);
        }

        // GET: id
        [HttpGet]
        [Route("GetAPublisher/{publisherId}")]
        public async Task<IActionResult> GetAPublisher(int publisherId)
        {
            var publisher = await _context.Publishers
                            .Include(x => x.Authors)
                            .FirstOrDefaultAsync(a => a.PublisherId == publisherId);

            if (publisher == null)
                return NotFound();

            return Ok(publisher);

        }


        // GET: id
        [HttpGet]
        [Route("GetAuthorsAttachedToAPublisher/{publisherId}")]
        public async Task<IActionResult> GetAuthorsAttachedToAPublisher(int publisherId)
        {
            var publisher = await _context.Authors
                                    .Where(a => a.PublisherId == publisherId).ToListAsync();

            if (publisher == null)
                return NotFound();

            return Ok(publisher);
        }

        // Post: /<controller>/
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(PublisherDTO publisherDTO)
        {
            var publisher = new Publisher { FirstName = publisherDTO.FirstName, LastName = publisherDTO.LastName };
            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();
            return Ok();
        }


        // Put: /<controller>/
        //[HttpPut]
        //public async Task<IActionResult> Put(PublisherDTO publisherData)
        //{
        //    if (publisherData == null || publisherData.PublisherId == 0)
        //        return BadRequest();

        //    var publisher = await _context.Publishers.FindAsync(publisherData.PublisherId);
        //    if (publisher == null)
        //        return NotFound();
        //    publisher.FirstName = publisherData.FirstName;
        //    publisher.LastName = publisherData.LastName;
        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}


        // Delete: /<controller>/
        //[HttpDelete("{publisherId}")]
        //public async Task<IActionResult> Delete(int publisherId)
        //{
        //    if (publisherId == 0)
        //        return BadRequest();

        //    var publisher = await _context.Publishers.FindAsync(publisherId);
        //    if (publisher == null)
        //        return NotFound();

        //    _context.Remove(publisher);
        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}
    }
}

