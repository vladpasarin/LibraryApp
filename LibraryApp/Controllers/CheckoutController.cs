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
    public class CheckoutController : ControllerBase
    {
        public readonly ICheckoutService _service;

        public CheckoutController(ICheckoutService service)
        {
            _service = service;
        }
        // GET: api/<CheckoutController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CheckoutController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var checkout = await _service.Get(id);
            if (checkout == null)
                return StatusCode(500);

            return Ok(checkout);
        }

        [HttpGet("{assetId}/{cardId}")]
        public async Task<IActionResult> GetByCardAndAsset(int assetId, int cardId)
        {
            var checkout = await _service.GetByCardAndAsset(assetId, cardId);
            if (checkout == null)
                return StatusCode(500);

            return Ok(checkout);
        }

        [HttpGet("user/{assetId}")]
        public async Task<IActionResult> GetCurrentUser(int assetId)
        {
            var user = await _service.GetCurrentUser(assetId);
            if (user == null)
                return StatusCode(500);

            return Ok(user);
        }

        [HttpGet("history/{assetId}")]
        public async Task<IActionResult> GetCheckoutHistory(int assetId)
        {
            var history = await _service.GetCheckoutHistory(assetId);
            if (history == null)
                return StatusCode(500);

            return Ok(history);
        }

        // POST api/<CheckoutController>
        [HttpPost]
        public async Task<IActionResult> Post(CheckoutDto newCheckoutDto)
        {
            return Ok(await _service.Add(newCheckoutDto));
        }

        [HttpPost("{assetId}/{cardId}")]
        public async Task<IActionResult> CheckOutItem(int assetId, int cardId)
        {
            return Ok(await _service.CheckOutItem(assetId, cardId));
        }

        [HttpPost("checkin/{assetId}")]
        public async Task<IActionResult> CheckInItem(int assetId)
        {
            return Ok(await _service.CheckInItem(assetId));
        }

        // PUT api/<CheckoutController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CheckoutController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
