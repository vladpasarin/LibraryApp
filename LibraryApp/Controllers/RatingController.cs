using LibraryApp.DTOs;
using LibraryApp.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryApp.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _service;

        public RatingController(IRatingService service)
        {
            _service = service;
        }

        // GET: api/<RatingController>
        [HttpGet("asset/{assetId}")]
        public async Task<IActionResult> GetAssetRatings(int assetId)
        {
            var ratings = await _service.GetAssetRatings(assetId);
            if (ratings == null)
                return NotFound("Asset has no ratings");
            return Ok(ratings);
        }

        // GET api/<RatingController>/5
        [Authorize]
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserRatings(int userId)
        {
            var ratings = await _service.GetUserRatings(userId);
            if (ratings == null)
                return NotFound("User has not given any ratings");
            return Ok(ratings);
        }

        [Authorize]
        [HttpGet("ratingExists/{userId}/{assetId}")]
        public async Task<IActionResult> RatingExists(int userId, int assetId)
        {
            var rating = await _service.RatingExists(userId, assetId);
            if (rating == null)
                return null;
            return Ok(rating);
        }

        // POST api/<RatingController>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(RatingDto ratingDto)
        {
            return Ok(await _service.Add(ratingDto));
        }

        // PUT api/<RatingController>/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRating(int id, RatingDto ratingDto)
        {
            return Ok(await _service.UpdateRating(id, ratingDto));
        }

        // DELETE api/<RatingController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
