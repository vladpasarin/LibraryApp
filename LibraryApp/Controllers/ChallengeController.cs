using LibraryApp.DTOs;
using LibraryApp.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengeController : ControllerBase
    {
        private readonly IChallengeService _service;

        public ChallengeController(IChallengeService service)
        {
            _service = service;
        }

        // GET: api/<ChallengeController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var challenges = await _service.GetAll();
            if (challenges == null)
                return StatusCode(500);
            return Ok(challenges);
        }

        // GET api/<ChallengeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var challenge = await _service.Get(id);
            if (challenge == null)
                return StatusCode(500);
            return Ok(challenge);
        }

        // POST api/<ChallengeController>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(ChallengeDto challengeDto)
        {
            return Ok(await _service.CreateAsync(challengeDto));
        }

        // PUT api/<ChallengeController>/5
        [HttpPost("start/{id}/{userId}")]
        public async Task<IActionResult> StartChallenge(int id, int userId)
        {
            return Ok(await _service.StartChallenge(id, userId));
        }

        // DELETE api/<ChallengeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
