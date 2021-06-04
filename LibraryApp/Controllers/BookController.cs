using LibraryApp.DTOs.Assets;
using LibraryApp.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        // GET: api/<BookController>
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var books = await _service.GetAll();
            if (books == null)
                return StatusCode(500);

            return Ok(books);
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _service.Get(id);
            if (book == null)
                return StatusCode(500);

            return Ok(book);
        }

        // POST api/<BookController>
        [HttpPost("add")]
        public async Task<IActionResult> Post(BookDto newBookDto)
        {
            return Ok(await _service.Add(newBookDto));
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
