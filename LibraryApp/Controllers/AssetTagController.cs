using LibraryApp.Entities.Assets.Tags;
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
    public class AssetTagController : ControllerBase
    {
        public readonly IAssetTagService _service;

        public AssetTagController(IAssetTagService service)
        {
            _service = service;
        }

        // GET: api/<AssetTagController>
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var assetTags = await _service.GetAll();
            if (assetTags == null)
            {
                return StatusCode(500);
            }
            return Ok(assetTags);
        }

        // GET api/<AssetTagController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var assetTag = await _service.Get(id);
            if (assetTag == null)
            {
                return StatusCode(500);
            }
            return Ok();
        }

        // POST api/<AssetTagController>
        [HttpPost]
        public async Task<IActionResult> Post(AssetTag newAssetTag)
        {
            return Ok(await _service.Add(newAssetTag));
        }

        // PUT api/<AssetTagController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AssetTagController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
