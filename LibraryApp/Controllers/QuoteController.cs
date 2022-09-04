using LibraryApp.DTOs;
using LibraryApp.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _service;

        public QuoteController(IQuoteService service)
        {
            _service = service;
        }

        // GET api/<QuoteController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var quote = await _service.GetById(id);
            if (quote == null)
                return StatusCode(404);
            return Ok(quote);
        }

        [HttpGet("bookQuotes/{bookId}")]
        public async Task<IActionResult> GetBookQuotes(int bookId)
        {
            var quotes = await _service.GetBookQuotes(bookId);
            return Ok(quotes);
        }

        // POST api/<QuoteController>
        [HttpPost]
        public async Task<IActionResult> Create(QuoteDto quoteDto)
        {
            return Ok(await _service.CreateAsync(quoteDto));
        }

        // PUT api/<QuoteController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<QuoteController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
