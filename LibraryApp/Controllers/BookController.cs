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

        //TODO: Make a get availability status method for books

        // GET: api/<BookController>
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var books = await _service.GetAll();
            if (books == null)
                return StatusCode(500);

            return Ok(books);
        }

        [HttpGet("ebook/all")]
        public async Task<IActionResult> GetAllEBooks()
        {
            var eBooks = await _service.GetAllEBooks();
            if (eBooks == null)
                return StatusCode(500);

            return Ok(eBooks);
        }

        [HttpGet("audiobook/all")]
        public async Task<IActionResult> GetAllAudioBooks()
        {
            var audioBooks = await _service.GetAllAudioBooks();
            if (audioBooks == null)
                return StatusCode(500);

            return Ok(audioBooks);
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

        [HttpGet("ebook/{id}")]
        public async Task<IActionResult> GetEBookById(int id)
        {
            var eBook = await _service.GetEBook(id);
            if (eBook == null)
                return StatusCode(500);

            return Ok(eBook);
        }

        [HttpGet("audiobook/{id}")]
        public async Task<IActionResult> GetAudioBookById(int id)
        {
            var audioBook = await _service.GetAudioBook(id);
            if (audioBook == null)
                return StatusCode(500);

            return Ok(audioBook);
        }

        [HttpGet("asset/{assetId:int}")]
        public async Task<IActionResult> GetBookByAssetId(int assetId)
        {
            var genericBook = await _service.GetGenericBook(assetId);
            if (genericBook == null)
                return StatusCode(500);

            return Ok(genericBook);
        }

        [HttpGet("generic")]
        public async Task<IActionResult> GetAllGenericBooks()
        {
            var genericBooks = await _service.GetAllGenericBooks();
            if (genericBooks == null)
                return StatusCode(500);

            return Ok(genericBooks);
        }

        [HttpGet("searchAuthor/{value}")]
        public async Task<IActionResult> SearchAuthor(string value)
        {
            return Ok(await _service.SearchBookAuthors(value));
        }

        [HttpGet("searchBookByAuthor/{value}")]
        public async Task<IActionResult> SearchBookByAuthor(string value)
        {
            return Ok(await _service.GetBooksByAuthor(value));
        }

        // POST api/<BookController>
        [HttpPost("add")]
        public async Task<IActionResult> Post(BookDto newBookDto)
        {
            return Ok(await _service.Add(newBookDto));
        }

        [HttpPost("ebook/add")]
        public async Task<IActionResult> Post(EBookDto newEBook)
        {
            return Ok(await _service.AddEBook(newEBook));
        }

        [HttpPost("audiobook/add")]
        public async Task<IActionResult> Post(AudioBookDto newAudioBook)
        {
            return Ok(await _service.AddAudioBook(newAudioBook));
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
