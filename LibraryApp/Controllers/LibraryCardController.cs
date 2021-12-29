using LibraryApp.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryCardController : ControllerBase
    {
        private readonly ILibraryCardService _service;

        public LibraryCardController(ILibraryCardService service)
        {
            _service = service;
        }

        // GET: api/<LibraryCardController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cards = await _service.GetAllLibraryCards();
            if (cards == null)
            {
                return StatusCode(500);
            }

            return Ok(cards);
        }

        // GET api/<LibraryCardController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var card = await _service.Get(id);
            if (card == null)
            {
                return StatusCode(500);
            }

            return Ok(card);
        }

        [HttpGet("/user/{id}")]
        public async Task<IActionResult> GetIdByUserId(int id)
        {
            var cardId = await _service.GetIdByUserId(id);
            if (cardId == 0)
            {
                return StatusCode(500);
            }

            return Ok(cardId);
        }

        // POST api/<LibraryCardController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LibraryCardController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LibraryCardController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
