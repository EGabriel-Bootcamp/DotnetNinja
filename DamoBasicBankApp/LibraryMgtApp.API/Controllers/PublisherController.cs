using LibraryManagementApp.DTOs;
using LibraryManagementApp.Entities;
using LibraryManagementApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementApp.Controllers
{
    
    public class PublisherController : RootController
    {
        private readonly IPublisher _publish;
        public PublisherController(IPublisher publish)
        {
            _publish = publish;
        }

        [HttpPost]
        [Route("CreatePublisher")]
        public async Task<IActionResult> CreatePublisher(CreatePublisherDTO publisher )
        {
            try
            {
                var response = await _publish.CreatePublisher(publisher);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while processing the request: " + ex.Message.ToString());
            }
        }

        [HttpGet]
        [Route("GetAllPublisher")]
        public async Task<IActionResult> GetAllPublisher()
        {
            try
            {
                var response = await _publish.GetAllPublisher();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while processing the request: " + ex.Message.ToString());
            }
        }


        [HttpGet]
        [Route("GetAPublisher/{id}")]
        public async Task<IActionResult> GetAPublisher(int id)
        {
            try
            {
                var response = await _publish.GetAPublisher(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while processing the request: " + ex.Message.ToString());
            }
        }


        [HttpGet]
        [Route("GetAllAuthorAttachedToPublisher/{id}")]
        public async Task<IActionResult> GetAllAuthorAttachedToPublisher(int id)
        {
            try
            {
                var response = await _publish.GetAllAuthorAttachedToPublisher(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while processing the request: " + ex.Message.ToString());
            }
        }
    }
}
