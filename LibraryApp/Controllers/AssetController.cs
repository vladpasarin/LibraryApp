using LibraryApp.DTOs.Assets;
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
    public class AssetController : ControllerBase
    {
        public readonly IAssetService _service;

        public AssetController(IAssetService service)
        {
            _service = service;
        }

        // GET: api/<AssetController>
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var assets = await _service.GetAll();
            if (assets == null)
                return StatusCode(500);

            return Ok(assets);
        }

        // GET api/<AssetController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var asset = await _service.Get(id);
            if (asset == null)
                return StatusCode(500);

            return Ok(asset);
        }

        // POST api/<AssetController>
        [HttpPost]
        public async Task<IActionResult> Post(AssetDto newAssetDto)
        {
            return Ok(await _service.Add(newAssetDto));
        }

        // PUT api/<AssetController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AssetController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
