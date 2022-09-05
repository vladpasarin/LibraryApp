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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HoldController : ControllerBase
    {
        private readonly IHoldService _service;

        public HoldController(IHoldService service)
        {
            _service = service;
        }

        // GET: api/<HoldController>
        [HttpGet("{assetId:int}")]
        public async Task<IActionResult> GetCurrentHolds(int assetId)
        {
            var holds = await _service.GetCurrentHolds(assetId);
            if (holds == null) return StatusCode(500);
            return Ok(holds);
        }

        // GET api/<HoldController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<HoldController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HoldController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HoldController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
