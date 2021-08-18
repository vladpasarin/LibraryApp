using LibraryApp.DTOs;
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
    public class BookmarkController : ControllerBase
    {
        public readonly IBookmarkService _service;

        public BookmarkController(IBookmarkService service)
        {
            _service = service;
        }

        // GET: api/<BookmarkController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookmarks = await _service.GetAll();
            if (bookmarks == null)
            {
                return StatusCode(500);
            }
            return Ok(bookmarks);
        }

        // GET api/<BookmarkController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var bookmark = await _service.Get(id);
            if (bookmark == null)
            {
                return StatusCode(500);
            }
            return Ok(bookmark);
        }

        [HttpGet("{userId}/{assetId}")]
        public async Task<IActionResult> FindByUserAndAssetIds(int userId, int assetId)
        {
            var bookmark = await _service.FindBookmarkByUserAndAsset(userId, assetId);
            if (bookmark == null)
            {
                return StatusCode(500);
            }
            return Ok(bookmark);
        }

        // POST api/<BookmarkController>
        [HttpPost]
        public async Task<IActionResult> Add(BookmarkDto newBookmark)
        {
            return Ok(await _service.Add(newBookmark));
        }

        // PUT api/<BookmarkController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BookmarkController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedBookmark = await _service.Get(id);
            if (deletedBookmark == null)
            {
                return StatusCode(500);
            }
            return Ok(await _service.Delete(deletedBookmark));
        }
    }
}
