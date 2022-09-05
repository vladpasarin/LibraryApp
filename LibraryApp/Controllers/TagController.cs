using LibraryApp.DTOs.Assets;
using LibraryApp.IServices;
using Microsoft.AspNetCore.Authorization;
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
    public class TagController : ControllerBase
    {
        public readonly ITagService _service;

        public TagController(ITagService service)
        {
            _service = service;
        }

        // GET: api/<TagController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tags = await _service.GetAll();
            if (tags == null)
                return StatusCode(500);

            return Ok(tags);
        }

        // GET api/<TagController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var tag = await _service.Get(id);
            if (tag == null)
                return StatusCode(500);

            return Ok(tag);
        }

        // POST api/<TagController>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(TagDto newTagDto)
        {
            return Ok(await _service.Add(newTagDto));
        }

        // PUT api/<TagController>/5
        [Authorize]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TagController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
